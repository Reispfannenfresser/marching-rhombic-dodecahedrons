using UnityEngine;

static class RDGrid {
	public static readonly int chunkSize = 8;
	private static Matrix4x4 fromGrid = new Matrix4x4(new Vector4(1, 0, -1, 0), new Vector4(1, 1, 0, 0), new Vector4(1, 0, 1, 0), new Vector4(0, 0, 0, 1));
	private static Matrix4x4 toGrid = new Matrix4x4(new Vector4(0.5f, 0, 0.5f, 0), new Vector4(-0.5f, 1, -0.5f, 0), new Vector4(-0.5f, 0, 0.5f, 0), new Vector4(0, 0, 0, 1));

	public static Vector3Int ToChunkPos(Vector3Int gridPos) {
		Vector3Int chunkPos = Vector3Int.zero;
		Vector3Int posInChunk = Vector3Int.zero;
		for(int i = 0; i < 3; i++) {
			chunkPos[i] = gridPos[i] / chunkSize;
			posInChunk[i] = gridPos[i] % chunkSize;
			if (gridPos[i] < 0 && posInChunk[i] != 0) {
				chunkPos[i] -= 1;
			}
		}

		return chunkPos;
	}

	public static Vector3Int ToPosInChunk(Vector3Int gridPos) {
		Vector3Int posInChunk = Vector3Int.zero;
		for(int i = 0; i < 3; i++) {
			posInChunk[i] = gridPos[i] % chunkSize;
			if (gridPos[i] < 0 && posInChunk[i] != 0) {
				posInChunk[i] += chunkSize;
			}
		}

		return posInChunk;
	}

	public static bool IsInChunk(Vector3Int posInChunk) {
		return posInChunk.x >= 0 && posInChunk.x < chunkSize && posInChunk.y >= 0 && posInChunk.y < chunkSize && posInChunk.z >= 0 && posInChunk.z < chunkSize;
	}

	public static Vector3Int FromChunkPos(Vector3Int chunkPos) {
		return new Vector3Int(chunkPos.x, chunkPos.y, chunkPos.z) * chunkSize;
	}

	public static Vector3Int FromChunkPos(Vector3Int chunkPos, Vector3Int posInChunk) {
		return new Vector3Int(chunkPos.x, chunkPos.y, chunkPos.z) * chunkSize + posInChunk;
	}

	public static Vector3 ToLocal(Vector3 gridPos) {
		return fromGrid.MultiplyVector(gridPos);
	}

	public static Vector3Int FromLocal(Vector3 localPos) {
		float roundedY = Mathf.Round(localPos.y * 0.5f) * 2;
		float remainingY = localPos.y - roundedY;

		Vector3 pos = toGrid.MultiplyVector(new Vector3(localPos.x, roundedY, localPos.z));

		if (Mathf.Abs(pos.x - Mathf.Round(pos.x)) + Mathf.Abs(remainingY) + Mathf.Abs(pos.z - Mathf.Round(pos.z)) > 1) {
			pos += (Vector3) toGrid.GetColumn(1) * ((remainingY > 0) ? 1 : -1);
		}

		return new Vector3Int((int) Mathf.Round(pos.x), (int) Mathf.Round(pos.y), (int) Mathf.Round(pos.z));
	}
}
