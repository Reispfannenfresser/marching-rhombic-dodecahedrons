using System;
using UnityEngine;
using Newtonsoft.Json;
using MeshData;

[System.Serializable]
public struct BlockModelFace {
	public readonly MeshData.MeshData mesh;
	public readonly FaceDirection[] culledAs;

	[JsonConstructor]
	public BlockModelFace(MeshData.MeshData mesh, FaceDirection[] culledAs) {
		this.mesh = mesh;
		this.culledAs = (culledAs == null) ? new FaceDirection[0] : culledAs;
	}
}

[System.Serializable]
public struct BlockModelData {
	public readonly BlockModelFace[] faces;
	public readonly bool[] culls;
	public readonly float[] loduv;

	public bool Culls(FaceDirection dir) {
		return culls[(int) dir.GetOpposite()];
	}

	[JsonConstructor]
	public BlockModelData(BlockModelFace[] faces, FaceDirection[] culls, float[] loduv) {
		this.faces = (faces == null) ? new BlockModelFace[0] : faces;
		bool[] tmp = new bool[12] {false, false, false, false, false, false, false, false, false, false, false, false};
		if (culls != null) {
			foreach (FaceDirection cull in culls) {
				tmp[(int) cull] = true;
			}
		}
		this.culls = tmp;
		this.loduv = (loduv == null || loduv.Length != 4) ? null : loduv;
	}
}
