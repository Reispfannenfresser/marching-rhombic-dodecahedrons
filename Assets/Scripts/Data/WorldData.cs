using UnityEngine;
using System;
using System.Collections.Generic;

namespace MRD.Data {
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
					throw new ArgumentException("An item with the same key has already been added. Key: " + chunkPos);
				}
				obj.data.Add(chunkPos, chunkData);
				obj.OnChunkDataAdded?.Invoke(chunkPos);
			}
		}

		public class BlockIndexer : Indexer3D<WorldData, BlockData> {
			public BlockIndexer(WorldData worldData) : base(worldData) {}

			protected override BlockData Get(Vector3Int blockPos) {
				Vector3Int chunkPos = RDGrid.ToChunkPos(blockPos);
				if (obj.data.ContainsKey(chunkPos)) {
					Vector3Int posInChunk = RDGrid.ToPosInChunk(blockPos);
					return obj.data[chunkPos].blocks[posInChunk];
				}

				return null;
			}

			protected override void Set(Vector3Int blockPos, BlockData blockData) {
				Vector3Int chunkPos = RDGrid.ToChunkPos(blockPos);

				if (obj.data.ContainsKey(chunkPos)) {
					Vector3Int posInChunk = RDGrid.ToPosInChunk(blockPos);
					obj.data[chunkPos].blocks[posInChunk] = blockData;
					obj.OnBlockDataChanged?.Invoke(blockPos);
				}

				//TODO remember data of blocks that would have ended up outside of the world
			}
		}

		// data
		private readonly Dictionary<Vector3Int, ChunkData> data = new Dictionary<Vector3Int, ChunkData>();

		// indexers
		public readonly ChunkIndexer chunks;
		public readonly BlockIndexer blocks;

		// events
		public delegate void BlockDataChanged(Vector3Int blockPos);
		public event BlockDataChanged OnBlockDataChanged;
		public delegate void ChunkDataAdded(Vector3Int chunkPos);
		public event ChunkDataAdded OnChunkDataAdded;

		public WorldData() {
			this.chunks = new ChunkIndexer(this);
			this.blocks = new BlockIndexer(this);
		}
	}
}
