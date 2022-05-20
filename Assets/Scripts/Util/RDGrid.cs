using UnityEngine;

static class RDGrid {
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
