using UnityEngine;
using System.Collections.Generic;

class World : MonoBehaviour {
	[field: SerializeField]
	public Vector3Int chunkSize {get; private set;} = new Vector3Int(10, 10, 10);
	[field: SerializeField]
	public int chunkRendererCount {get; private set;} = 1;
	[field: SerializeField]
	public GameObject chunkRenderer {get; private set;} = null;

	Dictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();

	void Awake() {
		GenerateChunk(Vector3Int.zero);
		RenderChunk(Vector3Int.zero);
	}

	void GenerateChunk(Vector3Int chunkPos) {
		chunks.Add(chunkPos, new Chunk(this, chunkPos));
	}

	void RenderChunk(Vector3Int chunkPos) {
		if (!chunks.ContainsKey(chunkPos)) {
			return;
		}

		Vector3 pos = new Vector3Int(chunkPos.x * chunkSize.x, chunkPos.y * chunkSize.y, chunkPos.z * chunkSize.z);
		GameObject newChunkRenderer = Instantiate(chunkRenderer, pos, Quaternion.identity, transform);
		newChunkRenderer.GetComponent<MeshFilter>().mesh = chunks[chunkPos].mesh;
	}

	public static Vector3 GridToLocal(Vector3Int gridPos) {
		Vector3 pos = gridPos.x * new Vector3(1, 0, -1) + gridPos.y * new Vector3(1, 1, 0) + gridPos.z * new Vector3(1, 0, 1);
		return pos;
	}

	public static Vector3Int LocalToGrid(Vector3 localPos) {
		float roundedY = Mathf.Round(localPos.y * 0.5f) * 2;
		float remainingY = localPos.y - roundedY;

		Vector3 pos = localPos.x * new Vector3(0.5f, 0, 0.5f) + roundedY * new Vector3(-0.5f, 1, -0.5f) + localPos.z * new Vector3(-0.5f, 0, 0.5f);

		if (Mathf.Abs(pos.x - Mathf.Round(pos.x)) + Mathf.Abs(remainingY) + Mathf.Abs(pos.z - Mathf.Round(pos.z)) > 1) {
			pos += new Vector3(-0.5f, 1, -0.5f) * ((remainingY > 0) ? 1 : -1);
		}

		return new Vector3Int((int) Mathf.Round(pos.x), (int) Mathf.Round(pos.y), (int) Mathf.Round(pos.z));
	}

	public BlockState GetBlockState(Vector3Int position) {
		Vector3Int chunkPos = Vector3Int.zero;
		Vector3Int blockPos = Vector3Int.zero;
		for(int i = 0; i < 3; i++) {
			chunkPos[i] = position[i] / chunkSize[i];
			blockPos[i] = position[i] % chunkSize[i];
			if (position[i] < 0 && blockPos[i] != 0) {
				chunkPos[i] -= 1;
				blockPos[i] += chunkSize[i];
			}
		}

		if (chunks.ContainsKey(chunkPos)) {
			return chunks[chunkPos].GetBlockState(blockPos);
		}

		return null;
	}
}
