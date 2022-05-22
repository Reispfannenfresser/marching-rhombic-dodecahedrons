using UnityEngine;
using System.Collections.Generic;
using System;

public class ChunkData {
	public class BlockIndexer : Indexer3D<ChunkData, BlockData> {
		public BlockIndexer(ChunkData chunkData) : base(chunkData) {}

		protected override BlockData Get(Vector3Int blockPos) {
			if (blockPos.x >= 0 && blockPos.x < obj.worldData.chunkSize.x && blockPos.y >= 0 && blockPos.y < obj.worldData.chunkSize.z && blockPos.z >= 0 && blockPos.z < obj.worldData.chunkSize.z) {
				return obj.data[blockPos.x, blockPos.y, blockPos.z];
			}

			return obj.worldData.blocks[obj.position + blockPos];
		}

		protected override void Set(Vector3Int blockPos, BlockData value) {
			if (blockPos.x >= 0 && blockPos.x < obj.worldData.chunkSize.x && blockPos.y >= 0 && blockPos.y < obj.worldData.chunkSize.z && blockPos.z >= 0 && blockPos.z < obj.worldData.chunkSize.z) {
				obj.data[blockPos.x, blockPos.y, blockPos.z] = value;
			} else {
				obj.worldData.blocks[obj.position + blockPos] = value;
			}
		}
	}

	public readonly WorldData worldData;
	public readonly Vector3Int position;
	public readonly Vector3Int chunkPosition;
	public readonly BlockIndexer blocks;
	private BlockData[,,] data;

	public ChunkData(WorldData worldData, Vector3Int chunkPos, BlockData[,,] data) {
		this.worldData = worldData;
		this.chunkPosition = chunkPos;
		this.position = new Vector3Int(worldData.chunkSize.x * chunkPos.x, worldData.chunkSize.y * chunkPos.y, worldData.chunkSize.z * chunkPos.z);
		this.data = data;
		this.blocks = new BlockIndexer(this);
	}
}
