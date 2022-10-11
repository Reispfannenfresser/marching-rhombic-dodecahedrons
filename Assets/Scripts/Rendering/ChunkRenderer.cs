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
		private MeshFilter meshFilter;
		private MeshCollider meshCollider;

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
				UpdateMeshes();
			}
		}

		private void Awake()
		{
			meshFilter = GetComponent<MeshFilter>();
			meshCollider = GetComponent<MeshCollider>();
		}

		public void UpdateMeshes()
		{
			Debug.Log("Updating Mesh of: " + chunkPos);

			ChunkData chunkData = GameController.instance.worldData.chunks[chunkPos];
			if (chunkData == null)
			{
				chunkData = new ChunkData(chunkPos, new BlockData[RDGrid.chunkSize, RDGrid.chunkSize, RDGrid.chunkSize]);
			}
			meshFilter.mesh = ChunkMeshGenerator.GenerateMesh(chunkData);

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
