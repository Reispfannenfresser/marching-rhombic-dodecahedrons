using UnityEngine;
using System.Collections.Generic;
using System;

public class ChunkData {
	public readonly WorldData worldData;
	public readonly Vector3Int position;
	public readonly Vector3Int chunkPosition;
	private BlockData[,,] blocks;

	public ChunkData(WorldData worldData, Vector3Int chunkPos) {
		this.worldData = worldData;
		this.chunkPosition = chunkPos;
		this.position = new Vector3Int(worldData.chunkSize.x * chunkPos.x, worldData.chunkSize.y * chunkPos.y, worldData.chunkSize.z * chunkPos.z);
		this.blocks = new BlockData[worldData.chunkSize.x, worldData.chunkSize.y, worldData.chunkSize.z];
		for(int x = 0; x < worldData.chunkSize.x; x++) {
			for(int y = 0; y < worldData.chunkSize.y; y++) {
				for(int z = 0; z < worldData.chunkSize.z; z++) {
					blocks[x, y, z] = new BlockData(new Vector3Int(x, y, z), true);
				}
			}
		}
	}

	public BlockData GetBlockData(Vector3Int blockPos) {
		if (blockPos.x >= 0 && blockPos.x < worldData.chunkSize.x && blockPos.y >= 0 && blockPos.y < worldData.chunkSize.z && blockPos.z >= 0 && blockPos.z < worldData.chunkSize.z) {
			return blocks[blockPos.x, blockPos.y, blockPos.z];
		}

		return worldData.GetBlockData(position + blockPos);
	}

	public void SetBlockData(Vector3Int blockPos, BlockData blockData) {
		if (blockPos.x >= 0 && blockPos.x < worldData.chunkSize.x && blockPos.y >= 0 && blockPos.y < worldData.chunkSize.z && blockPos.z >= 0 && blockPos.z < worldData.chunkSize.z) {
			blocks[blockPos.x, blockPos.y, blockPos.z] = blockData;
		} else {
			worldData.SetBlockData(position + blockPos, blockData);
		}
	}
}
