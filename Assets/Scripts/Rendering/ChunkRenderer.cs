using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class ChunkRenderer : MonoBehaviour {
	private MeshFilter meshFilter = null;
	private Vector3Int _chunkPos = Vector3Int.zero;
	public Vector3Int chunkPos {
		get {
			return _chunkPos;
		}
		set {
			_chunkPos = value;

			transform.position = RDGrid.ToLocal(RDGrid.FromChunkPos(chunkPos));
			UpdateMesh();
		}
	}

	private void Awake() {
		meshFilter = GetComponent<MeshFilter>();
	}

	public void UpdateMesh() {
		Debug.Log("Updating Mesh of: " + chunkPos);

		ChunkData chunkData = GameController.instance.worldData.chunks[chunkPos];
		if (chunkData != null) {
			meshFilter.mesh = ChunkMeshGenerator.GenerateMesh(chunkData);
		}
		else {
			meshFilter.mesh.Clear();
		}
	}
}
