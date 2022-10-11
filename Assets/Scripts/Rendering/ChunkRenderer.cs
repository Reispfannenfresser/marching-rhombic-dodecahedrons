using UnityEngine;
using System;
using System.Collections.Generic;
using MRD.Data;

namespace MRD.Rendering
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshCollider))]
	public class ChunkRenderer : MonoBehaviour
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
				UpdateMesh();
			}
		}

		private Vector3Int _chunkPos = Vector3Int.zero;
		public Vector3Int chunkPos
		{
			get
			{
				return _chunkPos;
			}
			set
			{
				_chunkPos = value;

				transform.position = RDGrid.ToLocal(RDGrid.FromChunkPos(chunkPos));
				UpdateMesh();
			}
		}

		private MeshFilter meshFilter;
		private MeshCollider meshCollider;

		private void Awake()
		{
			meshFilter = GetComponent<MeshFilter>();
			meshCollider = GetComponent<MeshCollider>();
		}

		public void Initialise(WorldData worldData, Vector3Int chunkPos)
		{
			if (initialised)
			{
				return;
			}

			_worldData = worldData;
			_chunkPos = chunkPos;

			initialised = true;

			transform.position = RDGrid.ToLocal(RDGrid.FromChunkPos(chunkPos));
			UpdateMesh();
		}

		public void UpdateMesh()
		{
			Debug.Log("Updating Mesh of: " + chunkPos);

			meshFilter.mesh = ChunkMeshGenerator.GenerateMesh(worldData, chunkPos);

			if (meshFilter.mesh.vertices.Length > 0)
			{
				meshCollider.sharedMesh = meshFilter.mesh;
			}
			else
			{
				meshFilter.mesh.Clear();
				if (meshCollider.sharedMesh != null)
				{
					meshCollider.sharedMesh.Clear();
				}
			}
		}
	}
}
