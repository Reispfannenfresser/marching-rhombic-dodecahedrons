using UnityEngine;

static class RDGrid {
	public static readonly Vector3Int chunkSize = new Vector3Int(10, 10, 10);

	public static Vector3Int ToChunkPos(Vector3Int gridPos) {
		Vector3Int chunkPos = Vector3Int.zero;
		Vector3Int posInChunk = Vector3Int.zero;
		for(int i = 0; i < 3; i++) {
			chunkPos[i] = gridPos[i] / chunkSize[i];
			posInChunk[i] = gridPos[i] % chunkSize[i];
			if (gridPos[i] < 0 && posInChunk[i] != 0) {
				chunkPos[i] -= 1;
			}
		}

		return chunkPos;
	}

	public static Vector3Int ToPosInChunk(Vector3Int gridPos) {
		Vector3Int posInChunk = Vector3Int.zero;
		for(int i = 0; i < 3; i++) {
			posInChunk[i] = gridPos[i] % chunkSize[i];
			if (gridPos[i] < 0 && posInChunk[i] != 0) {
				posInChunk[i] += chunkSize[i];
			}
		}

		return posInChunk;
	}

	public static Vector3Int FromChunkPos(Vector3Int chunkPos) {
		return new Vector3Int(chunkPos.x * chunkSize.x, chunkPos.y * chunkSize.y, chunkPos.z * chunkSize.z);
	}

	public static Vector3Int FromChunkPos(Vector3Int chunkPos, Vector3Int posInChunk) {
		return new Vector3Int(chunkPos.x * chunkSize.x, chunkPos.y * chunkSize.y, chunkPos.z * chunkSize.z) + posInChunk;
	}

	public static Vector3 ToLocal(Vector3Int gridPos) {
		return gridPos.x * new Vector3(1, 0, -1) + gridPos.y * new Vector3(1, 1, 0) + gridPos.z * new Vector3(1, 0, 1);
	}

	public static Vector3Int FromLocal(Vector3 localPos) {
		float roundedY = Mathf.Round(localPos.y * 0.5f) * 2;
		float remainingY = localPos.y - roundedY;

		Vector3 pos = localPos.x * new Vector3(0.5f, 0, 0.5f) + roundedY * new Vector3(-0.5f, 1, -0.5f) + localPos.z * new Vector3(-0.5f, 0, 0.5f);

		if (Mathf.Abs(pos.x - Mathf.Round(pos.x)) + Mathf.Abs(remainingY) + Mathf.Abs(pos.z - Mathf.Round(pos.z)) > 1) {
			pos += new Vector3(-0.5f, 1, -0.5f) * ((remainingY > 0) ? 1 : -1);
		}

		return new Vector3Int((int) Mathf.Round(pos.x), (int) Mathf.Round(pos.y), (int) Mathf.Round(pos.z));
	}
}
