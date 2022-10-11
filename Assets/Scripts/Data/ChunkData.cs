using UnityEngine;
using System.Collections.Generic;
using System;

namespace MRD.Data
{
	public class ChunkData
	{
		public class BlockIndexer : Indexer3D<ChunkData, BlockData>
		{
			public BlockIndexer(ChunkData chunkData) : base(chunkData) { }

			protected override BlockData Get(Vector3Int posInChunk)
			{
				if (posInChunk.x >= 0 && posInChunk.x < RDGrid.chunkSize && posInChunk.y >= 0 && posInChunk.y < RDGrid.chunkSize && posInChunk.z >= 0 && posInChunk.z < RDGrid.chunkSize)
				{
					return obj.data[posInChunk.x, posInChunk.y, posInChunk.z];
				}

				return GameController.instance.worldData.blocks[RDGrid.FromChunkPos(obj.chunkPos, posInChunk)];
			}

			protected override void Set(Vector3Int posInChunk, BlockData value)
			{
				if (RDGrid.IsInChunk(posInChunk))
				{
					obj.data[posInChunk.x, posInChunk.y, posInChunk.z] = value;
				}
				else
				{
					GameController.instance.worldData.blocks[RDGrid.FromChunkPos(obj.chunkPos, posInChunk)] = value;
				}
			}
		}

		public readonly Vector3Int chunkPos;
		public readonly BlockIndexer blocks;
		private BlockData[,,] data;

		public ChunkData(Vector3Int chunkPos, BlockData[,,] data)
		{
			this.chunkPos = chunkPos;
			this.data = data;
			this.blocks = new BlockIndexer(this);
		}
	}
}
