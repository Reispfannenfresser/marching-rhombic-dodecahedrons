using UnityEngine;
using System.Collections.Generic;
using System;

public class WorldRenderer : MonoBehaviour {
	[SerializeField]
	private float meshUpdateInterval = 0.1f;
	[SerializeField]
	private float rendererCreationInterval = 0.1f;

	private float currentMeshUpdateCooldown = 0f;
	private float currentRendererCreationCooldown = 0f;

	[SerializeField]
	private GameObject chunkRenderer = null;

	private WorldData worldData {
		get {
			return GameController.instance.worldData;
		}
	}

	private ISet<Vector3Int?> toRender = new HashSet<Vector3Int?>();

	public void MarkForRendering(Vector3Int chunkPos) {
		if (!ChunkRenderer.renderers.ContainsKey(chunkPos)) {
			toRender.Add(chunkPos);
		}
	}

	private void CreateRenderer(ChunkData chunkData) {
		Debug.Log("Creating Renderer for: " + chunkData.chunkPosition);

		GameObject newGameObject = Instantiate(chunkRenderer, Vector3.zero, Quaternion.identity, transform);
		ChunkRenderer newChunkRenderer = newGameObject.GetComponent<ChunkRenderer>();
		newChunkRenderer.chunkData = chunkData;
	}

	void Awake() {
		worldData.OnBlockDataChanged += OnBlockDataChanged;
		worldData.OnChunkDataAdded += OnChunkDataAdded;
	}

	private void OnChunkDataAdded(Vector3Int chunkPos) {
		foreach (ChunkNeighborDirection dir in Enum.GetValues(typeof(ChunkNeighborDirection))) {
			Vector3Int neighborPos = chunkPos + dir.GetVector();
			if (ChunkRenderer.renderers.ContainsKey(neighborPos)) {
				ChunkRenderer.renderers[neighborPos].dirty = true;
			}
		}
	}

	private void OnBlockDataChanged(Vector3Int position) {
		Vector3Int chunkPos = Vector3Int.zero;
		Vector3Int blockPos = Vector3Int.zero;
		for(int i = 0; i < 3; i++) {
			chunkPos[i] = position[i] / worldData.chunkSize[i];
			blockPos[i] = position[i] % worldData.chunkSize[i];
			if (position[i] < 0 && blockPos[i] != 0) {
				chunkPos[i] -= 1;
				blockPos[i] +=worldData.chunkSize[i];
			}
		}

		if (ChunkRenderer.renderers.ContainsKey(chunkPos)) {
			ChunkRenderer.renderers[chunkPos].dirty = true;
		}

		foreach (FaceDirection dir in Enum.GetValues(typeof(FaceDirection))) {
			Vector3Int neighborPos = position + dir.GetVector();
			for(int i = 0; i < 3; i++) {
				chunkPos[i] = neighborPos[i] / worldData.chunkSize[i];
				blockPos[i] = neighborPos[i] % worldData.chunkSize[i];
				if (neighborPos[i] < 0 && blockPos[i] != 0) {
					neighborPos[i] -= 1;
					neighborPos[i] += worldData.chunkSize[i];
				}
			}

			if (ChunkRenderer.renderers.ContainsKey(chunkPos)) {
				ChunkRenderer.renderers[chunkPos].dirty = true;
			}
		}
	}

	void FixedUpdate() {
		currentMeshUpdateCooldown -= Time.deltaTime;
		while (currentMeshUpdateCooldown <= 0) {
			currentMeshUpdateCooldown += meshUpdateInterval;

			IEnumerator<Vector3Int?> enumerator = ChunkRenderer.dirtyRenderers.GetEnumerator();
			enumerator.MoveNext();
			if (enumerator.Current.HasValue && ChunkRenderer.renderers.ContainsKey(enumerator.Current.Value)) {
				ChunkRenderer.renderers[enumerator.Current.Value].dirty = false;
			}
		}

		currentRendererCreationCooldown -= Time.deltaTime;
		while (currentRendererCreationCooldown <= 0) {
			currentRendererCreationCooldown += rendererCreationInterval;

			IEnumerator<Vector3Int?> enumerator = toRender.GetEnumerator();
			// skip chunks that are not yet generated
			do {
				enumerator.MoveNext();
				if (!enumerator.Current.HasValue) {
					return;
				}
			} while(worldData.chunks[enumerator.Current.Value] == null);

			// remove chunks that are already rendered
			if (ChunkRenderer.renderers.ContainsKey(enumerator.Current)) {
				toRender.Remove(enumerator.Current);
				continue;
			}

			// create renderer for chunk
			CreateRenderer(worldData.chunks[enumerator.Current.Value]);
			toRender.Remove(enumerator.Current);
		}
	}
}
