using UnityEngine;
using MRD.Rendering;
using MRD.Data;

[RequireComponent(typeof(WorldRenderer))]
class GameController : MonoBehaviour
{
	public readonly WorldData worldData = new WorldData();
	public WorldRenderer worldRenderer { get; private set; } = null;
	public static GameController instance = null;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("A GameController instance already exist!");
			return;
		}
		else
		{
			instance = this;
		}

		worldRenderer = GetComponent<WorldRenderer>();
	}

	void Start()
	{
		worldData.blocks[Vector3Int.zero] = new BlockData(Vector3Int.zero, Blocks.INDESTRUCTIBLE);
	}
}
