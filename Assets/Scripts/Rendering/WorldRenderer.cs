using UnityEngine;
using System.Collections.Generic;

public class WorldRenderer : MonoBehaviour {
	[SerializeField]
	private int maxChunkMeshUpdates = 1;
	[SerializeField]
	private GameObject chunkRenderer = null;

	private Dictionary<Vector3Int, ChunkRenderer> chunkRenderers = new Dictionary<Vector3Int, ChunkRenderer>();

	public void RenderChunk(ChunkData chunkData) {
		if (chunkRenderers.ContainsKey(chunkData.chunkPosition)) {
			return;
		}

		GameObject newGameObject = Instantiate(chunkRenderer, Vector3.zero, Quaternion.identity, transform);
		ChunkRenderer newChunkRenderer = newGameObject.GetComponent<ChunkRenderer>();
		newChunkRenderer.chunkData = chunkData;
		chunkRenderers.Add(chunkData.chunkPosition, newChunkRenderer);
	}

	public void Update() {
		for (int i = 0; i < maxChunkMeshUpdates; i++) {
			IEnumerator<ChunkRenderer> enumerator = ChunkRenderer.dirtyChunkRenderers.GetEnumerator();
			enumerator.MoveNext();
			if (enumerator.Current != null) {
				enumerator.Current.dirty = false;
			}
		}
	}
}
