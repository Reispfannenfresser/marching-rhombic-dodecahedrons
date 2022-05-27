using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(LODGroup))]
public class ChunkRenderer : MonoBehaviour {
	[SerializeField]
	private MeshFilter[] meshFilters = new MeshFilter[0];
	private LODGroup lodGroup = null;

	private Vector3Int _chunkPos = Vector3Int.zero;
	public Vector3Int chunkPos {
		get {
			return _chunkPos;
		}
		set {
			_chunkPos = value;

			transform.position = RDGrid.ToLocal(RDGrid.FromChunkPos(chunkPos));
			UpdateMeshes();
		}
	}

	private void Awake() {
		lodGroup = GetComponent<LODGroup>();

		if (meshFilters.Length != lodGroup.lodCount) {
			throw new ArgumentException("Number of LODs doesn't match the number of mesh filters provided.");
		}
	}

	public void UpdateMeshes() {
		Debug.Log("Updating Mesh of: " + chunkPos);

		ChunkData chunkData = GameController.instance.worldData.chunks[chunkPos];
		if (chunkData != null) {
			Mesh[] meshes = ChunkMeshGenerator.GenerateMeshes(chunkData, lodGroup.lodCount);

			for(int i = 0; i < lodGroup.lodCount; i++) {
				meshFilters[i].mesh = meshes[i];
			}
		}
		else {
			for(int i = 0; i < lodGroup.lodCount; i++) {
				meshFilters[i].mesh.Clear();
			}
		}
	}
}
