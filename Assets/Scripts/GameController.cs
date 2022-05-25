using UnityEngine;

[RequireComponent(typeof(WorldRenderer))]
[RequireComponent(typeof(WorldGenerator))]
class GameController : MonoBehaviour {
	[SerializeField]
	private Vector3Int size = Vector3Int.one;

	public readonly WorldData worldData = new WorldData();
	public WorldRenderer worldRenderer {get; private set;} = null;
	public WorldGenerator worldGenerator {get; private set;} = null;
	public static GameController instance = null;

	void Awake() {
		Blocks.LoadBlocks();

		worldRenderer = GetComponent<WorldRenderer>();
		worldGenerator = GetComponent<WorldGenerator>();

		if (instance != null) {
			Debug.LogError("A GameController instance already exist!");
			return;
		} else {
			instance = this;
		}

		for (int x = -size.x; x < size.x; x++) {
			for (int z = -size.z; z < size.z; z++) {
				for (int y = -size.y; y < size.y; y++) {
					Vector3Int pos = new Vector3Int(x, y, z);
					worldGenerator.MarkForGeneration(pos);
					worldRenderer.RenderChunk(pos);
				}
			}
		}
	}
}
