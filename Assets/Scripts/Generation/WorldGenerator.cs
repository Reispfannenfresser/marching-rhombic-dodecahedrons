using UnityEngine;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour {
	[SerializeField]
	private float chunkGenerationInterval = 0.1f;
	private float currentChunkGenerationCooldown = 0f;
	private ISet<Vector3Int?> toGenerate = new HashSet<Vector3Int?>();

	private WorldData worldData {
		get {
			return GameController.instance.worldData;
		}
	}

	public void MarkForGeneration(Vector3Int chunkPos) {
		toGenerate.Add(chunkPos);
	}

	private void GenerateChunk(Vector3Int chunkPos) {
		Debug.Log("Generating: " + chunkPos);

		BlockData[,,] blocks = new BlockData[worldData.chunkSize.x, worldData.chunkSize.y, worldData.chunkSize.z];
		for(int x = 0; x < worldData.chunkSize.x; x++) {
			for(int y = 0; y < worldData.chunkSize.y; y++) {
				for(int z = 0; z < worldData.chunkSize.z; z++) {
					blocks[x, y, z] = new BlockData(chunkPos.y * worldData.chunkSize.y + y <= 0);
				}
			}
		}
		GameController.instance.worldData.chunks[chunkPos] = new ChunkData(worldData, chunkPos, blocks);
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
				} else {
					toGenerate.Remove(enumerator.Current.Value);
				}
			}
		}
	}
}
