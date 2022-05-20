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

		worldData.GenerateChunk(new Vector3Int(0, 0, 0));
		worldData.GenerateChunk(new Vector3Int(0, 0, -1));
		worldData.GenerateChunk(new Vector3Int(0, -1, 0));
		worldData.GenerateChunk(new Vector3Int(0, -1, -1));
		worldData.GenerateChunk(new Vector3Int(-1, 0, 0));
		worldData.GenerateChunk(new Vector3Int(-1, 0, -1));
		worldData.GenerateChunk(new Vector3Int(-1, -1, 0));
		worldData.GenerateChunk(new Vector3Int(-1, -1, -1));

		worldRenderer.RenderChunk(worldData.GetChunkData(new Vector3Int(0, 0, 0)));
		worldRenderer.RenderChunk(worldData.GetChunkData(new Vector3Int(0, 0, -1)));
		worldRenderer.RenderChunk(worldData.GetChunkData(new Vector3Int(0, -1, 0)));
		worldRenderer.RenderChunk(worldData.GetChunkData(new Vector3Int(0, -1, -1)));
		worldRenderer.RenderChunk(worldData.GetChunkData(new Vector3Int(-1, 0, 0)));
		worldRenderer.RenderChunk(worldData.GetChunkData(new Vector3Int(-1, 0, -1)));
		worldRenderer.RenderChunk(worldData.GetChunkData(new Vector3Int(-1, -1, 0)));
		worldRenderer.RenderChunk(worldData.GetChunkData(new Vector3Int(-1, -1, -1)));
	}
}
