using UnityEngine;
using System.Collections.Generic;
using System;
using MRD.Data;

namespace MRD.Rendering
{
	public class WorldRenderer : MonoBehaviour
	{
		private bool initialised = false;

		private WorldData _worldData = null;
		public WorldData worldData
		{
			get
			{
				return _worldData;
			}
			set
			{
				_worldData = value;
				UpdateRenderers();
			}
		}

		[SerializeField]
		private GameObject chunkRenderer = null;
		protected Dictionary<Vector3Int, ChunkRenderer> chunkRenderers = new Dictionary<Vector3Int, ChunkRenderer>();

		void Awake()
		{
			GameController.instance.worldData.OnBlockDataChanged += OnBlockDataChanged;
			GameController.instance.worldData.OnChunkDataAdded += OnChunkDataAdded;
		}

		public void Initialise(WorldData worldData)
		{
			if (initialised)
			{
				return;
			}

			_worldData = worldData;

			initialised = true;

			UpdateRenderers();
		}

		public void RenderChunk(Vector3Int chunkPos)
		{
			if (!chunkRenderers.ContainsKey(chunkPos))
			{
				Debug.Log("Creating renderer for: " + chunkPos);

				GameObject newGameObject = Instantiate(chunkRenderer, Vector3.zero, Quaternion.identity, transform);
				newGameObject.name = "" + chunkPos;

				ChunkRenderer newChunkRenderer = newGameObject.GetComponent<ChunkRenderer>();
				newChunkRenderer.Initialise(worldData, chunkPos);

				chunkRenderers.Add(chunkPos, newChunkRenderer);
			}
		}

		public void UpdateRenderers()
		{
			foreach (Vector3Int chunkPos in chunkRenderers.Keys)
			{
				Destroy(chunkRenderers[chunkPos].gameObject);
			}

			chunkRenderers.Clear();
		}

		private void OnChunkDataAdded(Vector3Int chunkPos)
		{
			ISet<Vector3Int> affectedChunkPositions = new HashSet<Vector3Int>();

			// cache affected chunk positions
			affectedChunkPositions.Add(chunkPos);
			foreach (ChunkNeighborDirection dir in Enum.GetValues(typeof(ChunkNeighborDirection)))
			{
				affectedChunkPositions.Add(chunkPos + dir.GetVector());
			}

			// update chunk meshes
			foreach (Vector3Int affectedChunkPos in affectedChunkPositions)
			{
				if (chunkRenderers.ContainsKey(affectedChunkPos))
				{
					chunkRenderers[affectedChunkPos].UpdateMesh();
				}
				else
				{
					RenderChunk(affectedChunkPos);
				}
			}
		}

		private void OnBlockDataChanged(Vector3Int blockPos)
		{
			ISet<Vector3Int> affectedChunkPositions = new HashSet<Vector3Int>();

			// cache affected chunk positions
			affectedChunkPositions.Add(RDGrid.ToChunkPos(blockPos));
			foreach (FaceDirection dir in Enum.GetValues(typeof(FaceDirection)))
			{
				affectedChunkPositions.Add(RDGrid.ToChunkPos(blockPos + dir.GetVector()));
			}
			foreach (CornerDirection dir in Enum.GetValues(typeof(CornerDirection)))
			{
				affectedChunkPositions.Add(RDGrid.ToChunkPos(blockPos + dir.GetVector()));
			}

			// update chunk meshes
			foreach (Vector3Int affectedChunkPos in affectedChunkPositions)
			{
				if (chunkRenderers.ContainsKey(affectedChunkPos))
				{
					chunkRenderers[affectedChunkPos].UpdateMesh();
				}
				else
				{
					RenderChunk(affectedChunkPos);
				}
			}
		}
	}
}
