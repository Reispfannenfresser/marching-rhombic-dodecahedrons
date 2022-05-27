using System;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public struct VertexInformation {
	public readonly Vector3 position;
	public readonly Vector2 uv;

	[JsonConstructor]
	public VertexInformation(float[] position, float[] uv) {
		this.position = (position == null || position.Length != 3) ? default(Vector3) : new Vector3(position[0], position[1], position[2]);
		this.uv = (uv == null || uv.Length != 2) ? default(Vector2) : new Vector2(uv[0], uv[1]);
	}
}

[System.Serializable]
public struct FaceInformation {
	public readonly VertexInformation[] vertexInformation;
	public readonly Vector3 normal;
	public readonly int[] triangles;

	[JsonConstructor]
	public FaceInformation(VertexInformation[] vertexInformation, int[] normal, int[] triangles) {
		this.vertexInformation = (vertexInformation == null) ? new VertexInformation[0] : vertexInformation;
		this.normal = (normal == null || normal.Length != 3) ? default(Vector3) : new Vector3Int(normal[0], normal[1], normal[2]);
		this.triangles = (triangles == null) ? new int[0] : triangles;
	}
}

[System.Serializable]
public struct BlockModelFace {
	public readonly FaceInformation faceInformation;
	public readonly FaceDirection[] culledAs;

	[JsonConstructor]
	public BlockModelFace(FaceInformation faceInformation, FaceDirection[] culledAs) {
		this.faceInformation = faceInformation;
		this.culledAs = (culledAs == null) ? new FaceDirection[0] : culledAs;
	}
}

[System.Serializable]
public struct BlockModelData {
	public readonly BlockModelFace[] blockfaces;
	public readonly bool[] culls;

	public bool Culls(FaceDirection dir) {
		return culls[(int) dir.GetOpposite()];
	}

	[JsonConstructor]
	public BlockModelData(BlockModelFace[] blockfaces, FaceDirection[] culls) {
		this.blockfaces = (blockfaces == null) ? new BlockModelFace[0] : blockfaces;
		bool[] tmp = new bool[12] {false, false, false, false, false, false, false, false, false, false, false, false};
		if (culls != null) {
			foreach (FaceDirection cull in culls) {
				tmp[(int) cull] = true;
			}
		}
		this.culls = tmp;
	}
}
