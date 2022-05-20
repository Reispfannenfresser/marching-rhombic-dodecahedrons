using UnityEngine;
using System;

[System.Serializable]
public enum FaceDirection : int {
	UL,
	UF,
	UR,
	UB,
	LF,
	FR,
	RB,
	BL,
	DL,
	DF,
	DR,
	DB
}

[System.Serializable]
public enum CornerDirection : int {
	U,
	L,
	F,
	R,
	B,
	D
}

public static class FaceDirectionExtensions {
	private static Vector3Int[] vectors = {
		// UL
		new Vector3Int(-1, 1, -1),
		// UF
		new Vector3Int(-1, 1, 0),
		// UR
		new Vector3Int(0, 1, 0),
		// UB
		new Vector3Int(0, 1, -1),
		// LF
		new Vector3Int(-1, 0, 0),
		// FR
		new Vector3Int(0, 0, 1),
		// RB
		new Vector3Int(1, 0, 0),
		// BL
		new Vector3Int(0, 0, -1),
		// DL
		new Vector3Int(0, -1, 0),
		// DF
		new Vector3Int(0, -1, 1),
		// DR
		new Vector3Int(1, -1, 1),
		// DB
		new Vector3Int(1, -1, 0),
	};

	public static Vector3Int GetVector(this FaceDirection dir) {
		return vectors[(int) dir];
	}
}

public static class CornerDirectionExtensions {
	private static Vector3Int[] vectors = {
		// U
		new Vector3Int(-1, 2, -1),
		// L
		new Vector3Int(-1, 0, -1),
		// F
		new Vector3Int(-1, 0, 1),
		// R
		new Vector3Int(1, 0, 1),
		// B
		new Vector3Int(1, 0, -1),
		// D
		new Vector3Int(1, -2, 1)
	};

	public static Vector3Int GetVector(this CornerDirection dir) {
		return vectors[(int) dir];
	}
}
