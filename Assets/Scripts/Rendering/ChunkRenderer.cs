using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class ChunkRenderer : MonoBehaviour {
	public static ISet<ChunkRenderer> dirtyChunkRenderers = new HashSet<ChunkRenderer>();
	public static ISet<ChunkRenderer> chunkRenderers = new HashSet<ChunkRenderer>();

	private Mesh mesh {
		get {
			return meshFilter.mesh;
		}
		set {
			meshFilter.mesh = value;
		}
	}

	private MeshFilter meshFilter = null;
	private bool _dirty = false;
	public bool dirty {
		get {
			return _dirty;
		}
		set {
			if (_dirty && !value) {
				mesh = ChunkMeshGenerator.GenerateMesh(chunkData);
				dirtyChunkRenderers.Remove(this);
			}
			if (!_dirty && value) {
				dirtyChunkRenderers.Add(this);
			}
			_dirty = value;
		}
	}

	private ChunkData _chunkData = null;
	public ChunkData chunkData {
		get {
			return _chunkData;
		}
		set {
			if (chunkData != null) {
				chunkData.worldData.OnBlockDataChanged -= OnBlockDataChanged;
			}

			mesh.Clear();
			transform.position = RDGrid.ToLocal(value.position);
			_chunkData = value;
			chunkData.worldData.OnBlockDataChanged += OnBlockDataChanged;
			dirty = true;
		}
	}

	private void OnBlockDataChanged(Vector3Int position, BlockData blockData) {
		Vector3Int chunkPos = Vector3Int.zero;
		Vector3Int blockPos = Vector3Int.zero;
		for(int i = 0; i < 3; i++) {
			chunkPos[i] = position[i] / chunkData.worldData.chunkSize[i];
			blockPos[i] = position[i] % chunkData.worldData.chunkSize[i];
			if (position[i] < 0 && blockPos[i] != 0) {
				chunkPos[i] -= 1;
				blockPos[i] += chunkData.worldData.chunkSize[i];
			}
		}

		if (chunkPos == chunkData.chunkPosition) {
			dirty = true;
			return;
		}

		foreach (FaceDirection dir in Enum.GetValues(typeof(FaceDirection))) {
			Vector3Int neighborPos = position + dir.GetVector();
			chunkPos = Vector3Int.zero;
			blockPos = Vector3Int.zero;
			for(int i = 0; i < 3; i++) {
				chunkPos[i] = neighborPos[i] / chunkData.worldData.chunkSize[i];
				blockPos[i] = neighborPos[i] % chunkData.worldData.chunkSize[i];
				if (neighborPos[i] < 0 && blockPos[i] != 0) {
					neighborPos[i] -= 1;
					neighborPos[i] += chunkData.worldData.chunkSize[i];
				}
			}

			if (chunkPos == chunkData.chunkPosition) {
				dirty = true;
				return;
			}
		}
	}

	private void Awake() {
		meshFilter = GetComponent<MeshFilter>();
		chunkRenderers.Add(this);
	}

	private void OnDestroy() {
		if (chunkData != null) {
			chunkData.worldData.OnBlockDataChanged -= OnBlockDataChanged;
		}

		chunkRenderers.Remove(this);
		dirtyChunkRenderers.Remove(this);
	}
}
