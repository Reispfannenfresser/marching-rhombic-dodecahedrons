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

		BlockData[,,] blocks = new BlockData[RDGrid.chunkSize.x, RDGrid.chunkSize.y, RDGrid.chunkSize.z];
		for(int x = 0; x < RDGrid.chunkSize.x; x++) {
			for(int y = 0; y < RDGrid.chunkSize.y; y++) {
				for(int z = 0; z < RDGrid.chunkSize.z; z++) {
					Vector3Int posInChunk = new Vector3Int(x, y, z);
					blocks[posInChunk.x, posInChunk.y, posInChunk.z] = new BlockData(RDGrid.FromChunkPos(chunkPos, posInChunk).y <= 0);
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
				} else {
					toGenerate.Remove(enumerator.Current.Value);
				}
			}
		}
	}
}
