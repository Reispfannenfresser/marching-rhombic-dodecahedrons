using UnityEngine;
using System.Collections.Generic;
using System;

public static class ChunkMeshGenerator {
	public static Mesh GenerateMesh(ChunkData chunkData) {
		Debug.Log("Updating Mesh of: " + chunkData.chunkPos);

		Mesh mesh = new Mesh();

		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector3> normals = new List<Vector3>();

		for(int x = 0; x < RDGrid.chunkSize.x; x++) {
			for(int y = 0; y < RDGrid.chunkSize.y; y++) {
				for(int z = 0; z < RDGrid.chunkSize.z; z++) {
					Vector3Int blockPos = new Vector3Int(x, y, z);
					if (chunkData.blocks[blockPos].solid) {
						int offset = vertices.Count;
						foreach (FaceDirection direction in Enum.GetValues(typeof(FaceDirection))) {
							BlockData neighbor = chunkData.blocks[blockPos + direction.GetVector()];
							if (neighbor == null || !neighbor.solid) {
								int triangleIndexOffset = vertices.Count;
								foreach(Vector3 vertex in MeshData.vertices[(int) direction]) {
									vertices.Add(vertex + RDGrid.ToLocal(blockPos));
									normals.Add(MeshData.normals[(int) direction]);
								}
								foreach(int triangleIndex in MeshData.triangles) {
									triangles.Add(triangleIndex + triangleIndexOffset);
								}
							}
						}
					}
				}
			}
		}

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.normals = normals.ToArray();

		return mesh;
	}
}
