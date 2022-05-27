using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public static class ChunkMeshGenerator {
	public static Mesh[] GenerateMeshes(ChunkData chunkData, int lodCount) {
		Mesh mesh = new Mesh();

		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector3> normals = new List<Vector3>();

		for(int x = 0; x < RDGrid.chunkSize.x; x++) {
			for(int y = 0; y < RDGrid.chunkSize.y; y++) {
				for(int z = 0; z < RDGrid.chunkSize.z; z++) {
					Vector3Int blockPos = new Vector3Int(x, y, z);
					BlockModelData model = BlockModels.GetBlockModel(chunkData.blocks[blockPos].block.id);
					int offset = vertices.Count;

					foreach (BlockModelFace face in model.blockfaces) {
						bool culled = true;

						foreach (FaceDirection faceDir in face.culledAs) {
							BlockData neighbor = chunkData.blocks[blockPos + faceDir.GetVector()];
							if (neighbor != null && !BlockModels.GetBlockModel(neighbor.block.id).Culls(faceDir)) {
								culled = false;
								break;
							}
						}

						if (culled) {
							continue;
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

		Mesh[] meshes = new Mesh[lodCount];
		for (int i = 0; i < lodCount; i++) {
			meshes[i] = mesh;
		}

		return meshes;
	}
}
