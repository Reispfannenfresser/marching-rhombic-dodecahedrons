using UnityEngine;
using System.Collections.Generic;

public class WorldData {
	public class ChunkIndexer : Indexer3D<WorldData, ChunkData> {
		public ChunkIndexer(WorldData worldData) : base(worldData) {}

		protected override ChunkData Get(Vector3Int chunkPos) {
			if (obj.data.ContainsKey(chunkPos)) {
				return obj.data[chunkPos];
			}

			return null;
		}

		protected override void Set(Vector3Int chunkPos, ChunkData chunkData) {
			if (obj.data.ContainsKey(chunkPos)) {
				Debug.LogError("Attempted to override an already existing chunk!");
				return;
			}
			obj.data.Add(chunkPos, chunkData);
		}
	}

	public class BlockIndexer : Indexer3D<WorldData, BlockData> {
		public BlockIndexer(WorldData worldData) : base(worldData) {}

		protected override BlockData Get(Vector3Int blockPos) {
			Vector3Int chunkPos = Vector3Int.zero;
			Vector3Int posInChunk = Vector3Int.zero;
			for(int i = 0; i < 3; i++) {
				chunkPos[i] = blockPos[i] / obj.chunkSize[i];
				posInChunk[i] = blockPos[i] % obj.chunkSize[i];
				if (blockPos[i] < 0 && posInChunk[i] != 0) {
					chunkPos[i] -= 1;
					posInChunk[i] += obj.chunkSize[i];
				}
			}

			if (obj.data.ContainsKey(chunkPos)) {
				return obj.data[chunkPos].blocks[posInChunk];
			}

			return null;
		}

		protected override void Set(Vector3Int blockPos, BlockData blockData) {
			Vector3Int chunkPos = Vector3Int.zero;
			Vector3Int posInChunk = Vector3Int.zero;
			for(int i = 0; i < 3; i++) {
				chunkPos[i] = blockPos[i] / obj.chunkSize[i];
				posInChunk[i] = blockPos[i] % obj.chunkSize[i];
				if (blockPos[i] < 0 && posInChunk[i] != 0) {
					chunkPos[i] -= 1;
					posInChunk[i] += obj.chunkSize[i];
				}
			}

			if (obj.data.ContainsKey(chunkPos)) {
				obj.data[chunkPos].blocks[posInChunk] = blockData;
				obj.OnBlockDataChanged(blockPos, blockData);
			}

			//TODO remember data of blocks that would have ended up outside of the world
		}
	}

	// data
	private readonly Dictionary<Vector3Int, ChunkData> data = new Dictionary<Vector3Int, ChunkData>();
	public readonly Vector3Int chunkSize;

	// indexers
	public readonly ChunkIndexer chunks;
	public readonly BlockIndexer blocks;

	// events
	public delegate void BlockChanged(Vector3Int blockPos, BlockData newBlockData);
	public event BlockChanged OnBlockDataChanged;

	public WorldData(Vector3Int chunkSize) {
		this.chunkSize = chunkSize;
		this.chunks = new ChunkIndexer(this);
		this.blocks = new BlockIndexer(this);
	}
}
