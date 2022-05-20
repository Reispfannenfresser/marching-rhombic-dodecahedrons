using UnityEngine;
using System.Collections.Generic;

public class WorldData {
	public readonly Vector3Int chunkSize;
	private readonly Dictionary<Vector3Int, ChunkData> chunks = new Dictionary<Vector3Int, ChunkData>();

	public delegate void BlockChanged(Vector3Int blockPos, BlockData newBlockData);
	public event BlockChanged OnBlockDataChanged;

	public WorldData(Vector3Int chunkSize) {
		this.chunkSize = chunkSize;
	}

	public void SetBlockData(Vector3Int position, BlockData blockData) {
		Vector3Int chunkPos = Vector3Int.zero;
		Vector3Int blockPos = Vector3Int.zero;
		for(int i = 0; i < 3; i++) {
			chunkPos[i] = position[i] / chunkSize[i];
			blockPos[i] = position[i] % chunkSize[i];
			if (position[i] < 0 && blockPos[i] != 0) {
				chunkPos[i] -= 1;
				blockPos[i] += chunkSize[i];
			}
		}

		if (chunks.ContainsKey(chunkPos)) {
			chunks[chunkPos].SetBlockData(blockPos, blockData);
			OnBlockDataChanged(position, blockData);
		}
	}

	public BlockData GetBlockData(Vector3Int position) {
		Vector3Int chunkPos = Vector3Int.zero;
		Vector3Int blockPos = Vector3Int.zero;
		for(int i = 0; i < 3; i++) {
			chunkPos[i] = position[i] / chunkSize[i];
			blockPos[i] = position[i] % chunkSize[i];
			if (position[i] < 0 && blockPos[i] != 0) {
				chunkPos[i] -= 1;
				blockPos[i] += chunkSize[i];
			}
		}

		if (chunks.ContainsKey(chunkPos)) {
			return chunks[chunkPos].GetBlockData(blockPos);
		}

		return null;
	}

	public ChunkData GetChunkData(Vector3Int chunkPos) {
		if (chunks.ContainsKey(chunkPos)) {
			return chunks[chunkPos];
		}
		return null;
	}

	public void GenerateChunk(Vector3Int chunkPos) {
		chunks.Add(chunkPos, new ChunkData(this, chunkPos));
	}
}
