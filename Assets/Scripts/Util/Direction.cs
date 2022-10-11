using UnityEngine;
using System;

[System.Serializable]
public enum FaceDirection : int
{
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
public enum CornerDirection : int
{
	U,
	L,
	F,
	R,
	B,
	D
}

[System.Serializable]
public enum ChunkNeighborDirection : int
{
	U,
	L,
	F,
	R,
	B,
	D,
	UL,
	DR,
	UF,
	DB,
	LF,
	RB
}

public static class FaceDirectionExtensions
{
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

	private static FaceDirection[] opposites = {
		// UL
		FaceDirection.DR,
		// UF
		FaceDirection.DB,
		// UR
		FaceDirection.DL,
		// UB
		FaceDirection.DF,
		// LF
		FaceDirection.RB,
		// FR
		FaceDirection.BL,
		// RB
		FaceDirection.LF,
		// BL
		FaceDirection.FR,
		// DL
		FaceDirection.UR,
		// DF
		FaceDirection.UB,
		// DR
		FaceDirection.UL,
		// DB
		FaceDirection.UF
	};

	public static Vector3Int GetVector(this FaceDirection dir)
	{
		return vectors[(int)dir];
	}

	public static FaceDirection GetOpposite(this FaceDirection dir)
	{
		return opposites[(int)dir];
	}
}

public static class CornerDirectionExtensions
{
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

	private static CornerDirection[] opposites = {
		CornerDirection.D,
		CornerDirection.R,
		CornerDirection.B,
		CornerDirection.L,
		CornerDirection.F,
		CornerDirection.U
	};

	public static Vector3Int GetVector(this CornerDirection dir)
	{
		return vectors[(int)dir];
	}

	public static CornerDirection GetOpposite(this CornerDirection dir)
	{
		return opposites[(int)dir];
	}
}

public static class ChunkNeighborDirectionExtensions
{
	private static Vector3Int[] vectors = {
		// U
		new Vector3Int(0, 1, 0),
		// L
		new Vector3Int(-1, 0, 0),
		// F
		new Vector3Int(0, 0, 1),
		// R
		new Vector3Int(1, 0, 0),
		// B
		new Vector3Int(0, 0, -1),
		// D
		new Vector3Int(0, -1, 0),
		// UR
		new Vector3Int(1, 1, 0),
		// DL
		new Vector3Int(-1, -1, 0),
		// UF
		new Vector3Int(0, 1, 1),
		// DB
		new Vector3Int(0, -1, -1),
		// LF
		new Vector3Int(1, 0, 1),
		// RB
		new Vector3Int(-1, 0, -1),
	};

	private static ChunkNeighborDirection[] opposites = {
		// U,
		ChunkNeighborDirection.D,
		// L,
		ChunkNeighborDirection.R,
		// F,
		ChunkNeighborDirection.B,
		// R,
		ChunkNeighborDirection.L,
		// B,
		ChunkNeighborDirection.F,
		// D,
		ChunkNeighborDirection.U,
		// UL,
		ChunkNeighborDirection.DR,
		// DR,
		ChunkNeighborDirection.UL,
		// UF,
		ChunkNeighborDirection.DB,
		// DB,
		ChunkNeighborDirection.UF,
		// LF,
		ChunkNeighborDirection.RB,
		// RB
		ChunkNeighborDirection.LF
	};

	public static Vector3Int GetVector(this ChunkNeighborDirection dir)
	{
		return vectors[(int)dir];
	}

	public static ChunkNeighborDirection GetOpposite(this ChunkNeighborDirection dir)
	{
		return opposites[(int)dir];
	}
}
