using UnityEngine;

[RequireComponent(typeof(WorldRenderer))]
class GameController : MonoBehaviour {
	public readonly WorldData worldData = new WorldData(new Vector3Int(10, 10, 10));
	public WorldRenderer worldRenderer {get; private set;} = null;
	public static GameController instance = null;

	void Awake() {
		worldRenderer = GetComponent<WorldRenderer>();

		if (instance != null) {
			Debug.LogError("A GameController instance already exist!");
			return;
		} else {
			instance = this;
		}

		for (int x = -1; x < 1; x++) {
			for (int y = -1; y < 1; y++) {
				for (int z = -1; z < 1; z++) {
					Vector3Int pos = new Vector3Int(x, y, z);
					worldData.chunks[pos] = new ChunkData(worldData, pos);
					worldRenderer.RenderChunk(worldData.chunks[pos]);
				}
			}
		}
	}
}
