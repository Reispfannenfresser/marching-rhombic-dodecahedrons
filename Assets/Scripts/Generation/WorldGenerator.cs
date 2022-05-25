using UnityEngine;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour {
	[SerializeField]
	private float chunkGenerationInterval = 0.05f;
	private float currentChunkGenerationCooldown = 0f;
	private ISet<Vector3Int?> toGenerate = new HashSet<Vector3Int?>();

	private ValueMap heightMap = new Transformed(new PerlinNoise(0), Vector3.zero, new Vector3(0.03f, 0.03f, 20f));

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
		return (localPos.y <= heightMap[localPos.x, localPos.z] ? Blocks.GetBlock("ground") : Blocks.GetBlock("air"));
	}

	private void GenerateChunk(Vector3Int chunkPos) {
		Debug.Log("Generating: " + chunkPos);

		BlockData[,,] blocks = new BlockData[RDGrid.chunkSize.x, RDGrid.chunkSize.y, RDGrid.chunkSize.z];
		for(int x = 0; x < RDGrid.chunkSize.x; x++) {
			for(int y = 0; y < RDGrid.chunkSize.y; y++) {
				for(int z = 0; z < RDGrid.chunkSize.z; z++) {
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
		while (currentChunkGenerationCooldown <= 0) {
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
