using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public static class ChunkMeshGenerator {
	public static Mesh GenerateMesh(ChunkData chunkData) {
		Mesh mesh = new Mesh();

		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector3> normals = new List<Vector3>();

		for(int x = 0; x < RDGrid.chunkSize.x; x++) {
			for(int y = 0; y < RDGrid.chunkSize.y; y++) {
				for(int z = 0; z < RDGrid.chunkSize.z; z++) {
					Vector3Int blockPos = new Vector3Int(x, y, z);
					BlockModel model = BlockModels.GetBlockModel(chunkData.blocks[blockPos].block.id);
					int offset = vertices.Count;

					foreach (BlockModelFace face in model.blockfaces) {
						if (face.culledAs.HasValue) {
							BlockData neighbor = chunkData.blocks[blockPos + face.culledAs.Value.GetVector()];
							if (neighbor != null && BlockModels.GetBlockModel(neighbor.block.id).Culls(face.culledAs.Value.GetOpposite())) {
								continue;
							}
						}

						int triangleIndexOffset = vertices.Count;

						foreach(VertexInformation vertex in face.faceInformation.vertexInformation) {
							vertices.Add(vertex.position + RDGrid.ToLocal(blockPos));
							normals.Add(face.faceInformation.normal);
						}
						foreach(int triangleIndex in face.faceInformation.triangles) {
							triangles.Add(triangleIndex + triangleIndexOffset);
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
