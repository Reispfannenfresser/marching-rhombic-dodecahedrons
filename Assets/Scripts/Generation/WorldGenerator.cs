using UnityEngine;
using System.Collections.Generic;
using MRD.Data;

namespace MRD.WorldGen
{
	public class WorldGenerator : MonoBehaviour
	{
		[SerializeField]
		private float chunkGenerationInterval = 0.05f;
		private float currentChunkGenerationCooldown = 0f;
		private ISet<Vector3Int?> toGenerate = new HashSet<Vector3Int?>();

		private WorldData worldData
		{
			get
			{
				return GameController.instance.worldData;
			}
		}

		public void MarkForGeneration(Vector3Int chunkPos)
		{
			toGenerate.Add(chunkPos);
		}

		private void GenerateChunk(Vector3Int chunkPos)
		{
			Debug.Log("Generating: " + chunkPos);

			BlockData[,,] blocks = new BlockData[RDGrid.chunkSize, RDGrid.chunkSize, RDGrid.chunkSize];
			for (int x = 0; x < RDGrid.chunkSize; x++)
			{
				for (int y = 0; y < RDGrid.chunkSize; y++)
				{
					for (int z = 0; z < RDGrid.chunkSize; z++)
					{
						Vector3Int posInChunk = new Vector3Int(x, y, z);
						Vector3Int gridPos = RDGrid.FromChunkPos(chunkPos, posInChunk);
						Block toPlace = (gridPos == Vector3.zero) ? Blocks.GetBlock("indestructible") : Blocks.GetBlock("air");
						blocks[posInChunk.x, posInChunk.y, posInChunk.z] = new BlockData(gridPos, toPlace);
					}
				}
			}
			worldData.chunks[chunkPos] = new ChunkData(chunkPos, blocks);
		}

		private void FixedUpdate()
		{
			currentChunkGenerationCooldown -= Time.deltaTime;
			if (currentChunkGenerationCooldown <= 0)
			{
				currentChunkGenerationCooldown += chunkGenerationInterval;

				IEnumerator<Vector3Int?> enumerator = toGenerate.GetEnumerator();
				enumerator.MoveNext();
				if (enumerator.Current.HasValue)
				{
					if (worldData.chunks[enumerator.Current.Value] == null)
					{
						GenerateChunk(enumerator.Current.Value);
					}
					toGenerate.Remove(enumerator.Current.Value);
				}
			}
		}
	}
}
