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
		Debug.Log("Creating Renderer for: " + chunkData.chunkPos);

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

	private void OnBlockDataChanged(Vector3Int blockPos) {
		Vector3Int chunkPos = RDGrid.ToChunkPos(blockPos);

		if (ChunkRenderer.renderers.ContainsKey(chunkPos)) {
			ChunkRenderer.renderers[chunkPos].dirty = true;
		}

		foreach (FaceDirection dir in Enum.GetValues(typeof(FaceDirection))) {
			Vector3Int neighborChunkPos = RDGrid.ToChunkPos(blockPos + dir.GetVector());

			if (ChunkRenderer.renderers.ContainsKey(neighborChunkPos)) {
				ChunkRenderer.renderers[neighborChunkPos].dirty = true;
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
