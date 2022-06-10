using UnityEngine;
using System.Collections.Generic;
using ValueMaps;
using MRD.Data;

namespace MRD.WorldGen {
	public class WorldGenerator : MonoBehaviour {
		private class TransformedFloat2D : Transformed<float, float, float, float> {
			public TransformedFloat2D(ValueMap<float, float> valueMap, Vector2 scale, float angle, float outputScale) :
				base(2, valueMap, (input) => new float[] {
					Mathf.Cos(angle) * scale[0] * input[0] - Mathf.Sin(angle) * scale[1] * input[1],
					Mathf.Sin(angle) * scale[0] * input[0] + Mathf.Cos(angle) * scale[1] * input[1]
				}, (value) => {
					return value * outputScale;
				}) {}
		}

		private class CombinedFloat2D : Combined<float, float> {
			public CombinedFloat2D(ValueMap<float, float>[] maps) : base(2, maps, (values) => {
				float sum = 0;
				foreach (float value in values) {
					sum += value;
				}
				return sum;
			}) {}
		}

		[SerializeField]
		private float chunkGenerationInterval = 0.05f;
		private float currentChunkGenerationCooldown = 0f;
		private ISet<Vector3Int?> toGenerate = new HashSet<Vector3Int?>();

		private static System.Random seedGenerator = new System.Random();

		private ValueMap<float, float> heightMap = new CombinedFloat2D(new ValueMap<float, float>[] {
			new TransformedFloat2D(new PerlinNoise2D(seedGenerator), Vector2.one * 0.05f, 75f * Mathf.Deg2Rad, 10),
			new TransformedFloat2D(new PerlinNoise2D(seedGenerator), Vector2.one * 0.01f, 45f * Mathf.Deg2Rad, 30),
			new TransformedFloat2D(new PerlinNoise2D(seedGenerator), Vector2.one * 0.002f, 15f * Mathf.Deg2Rad, 60)
		});

		private WorldData worldData {
			get {
				return GameController.instance.worldData;
			}
		}

		public void MarkForGeneration(Vector3Int chunkPos) {
			toGenerate.Add(chunkPos);
		}

		private Block GetBlock(Vector3Int gridPos) {
			Vector3 localPos = RDGrid.ToLocal(gridPos);
			return (localPos.y <= heightMap[new float[] {localPos.x, localPos.z}] ? Blocks.GetBlock("ground") : Blocks.GetBlock("air"));
		}

		private void GenerateChunk(Vector3Int chunkPos) {
			Debug.Log("Generating: " + chunkPos);

			BlockData[,,] blocks = new BlockData[RDGrid.chunkSize, RDGrid.chunkSize, RDGrid.chunkSize];
			for(int x = 0; x < RDGrid.chunkSize; x++) {
				for(int y = 0; y < RDGrid.chunkSize; y++) {
					for(int z = 0; z < RDGrid.chunkSize; z++) {
						Vector3Int posInChunk = new Vector3Int(x, y, z);
						Vector3Int gridPos = RDGrid.FromChunkPos(chunkPos, posInChunk);
						blocks[posInChunk.x, posInChunk.y, posInChunk.z] = new BlockData(gridPos, GetBlock(gridPos));
					}
				}
			}
			worldData.chunks[chunkPos] = new ChunkData(chunkPos, blocks);
		}

		private void FixedUpdate() {
			currentChunkGenerationCooldown -= Time.deltaTime;
			if (currentChunkGenerationCooldown <= 0) {
				currentChunkGenerationCooldown += chunkGenerationInterval;

				IEnumerator<Vector3Int?> enumerator = toGenerate.GetEnumerator();
				enumerator.MoveNext();
				if (enumerator.Current.HasValue) {
					if (worldData.chunks[enumerator.Current.Value] == null) {
						GenerateChunk(enumerator.Current.Value);
					}
					toGenerate.Remove(enumerator.Current.Value);
				}
			}
		}
	}
}
