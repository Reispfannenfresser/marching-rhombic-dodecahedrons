using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public static class ChunkMeshGenerator {
	public static Mesh[] GenerateMeshes(ChunkData chunkData, int lodCount) {
		Mesh[] meshes = new Mesh[lodCount];

		List<Vector3>[] vertices = new List<Vector3>[lodCount];
		List<int>[] triangles = new List<int>[lodCount];
		List<Vector2>[] uv = new List<Vector2>[lodCount];
		List<Vector3>[] normals = new List<Vector3>[lodCount];

		// create mesh data buffers
		for (int i = 0; i < lodCount; i++) {
			vertices[i] = new List<Vector3>();
			uv[i] = new List<Vector2>();
			normals[i] = new List<Vector3>();
			triangles[i] = new List<int>();
		}

		for(int x = 0; x < RDGrid.chunkSize; x++) {
			for(int y = 0; y < RDGrid.chunkSize; y++) {
				for(int z = 0; z < RDGrid.chunkSize; z++) {
					// main mesh
					Vector3Int blockPos = new Vector3Int(x, y, z);
					BlockModelData model = BlockModels.GetBlockModel(chunkData.blocks[blockPos].block.id);
					int offset = vertices[0].Count;

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

						int triangleIndexOffset = vertices[0].Count;

						foreach(VertexInformation vertex in face.faceInformation.vertexInformation) {
							vertices[0].Add(vertex.position + RDGrid.ToLocal(blockPos));
							uv[0].Add(vertex.uv);
						}
						foreach(int triangleIndex in face.faceInformation.triangles) {
							triangles[0].Add(triangleIndex + triangleIndexOffset);
						}
					}
				}
			}
		}

		// lods
		for (int i = 1; i < lodCount; i++) {
			int subGridSize = Math.Min(RDGrid.chunkSize, 1 << (i - 1));
			for(int x = 0; x < RDGrid.chunkSize; x += subGridSize) {
				for(int y = 0; y < RDGrid.chunkSize; y += subGridSize) {
					for(int z = 0; z < RDGrid.chunkSize; z += subGridSize) {
						Vector3Int SubGridPos = new Vector3Int(x, y, z);
						string blockID = chunkData.blocks[SubGridPos].block.id;
						BlockModelData model = BlockModels.GetBlockModel(blockID);
						if (model.loduv != null) {
							Dictionary<int, int> vertexIndices = new Dictionary<int, int>();
							Vector2 uvOffset = new Vector2(model.loduv[0], model.loduv[1]);
							Vector2 uvScale = new Vector2(model.loduv[2] - model.loduv[0], model.loduv[3] - model.loduv[1]);

							foreach (LODMesh.LODFace lodFace in LODMesh.faces) {
								Vector3Int neighborPos = SubGridPos + lodFace.culledAs.GetVector() * subGridSize;
								BlockData neighbor = chunkData.blocks[neighborPos];
								if (neighbor == null || RDGrid.IsInChunk(neighborPos) && BlockModels.GetBlockModel(neighbor.block.id).loduv != null) {
									continue;
								}

								foreach (int vertexIndex in lodFace.vertexIndices) {
									if (!vertexIndices.ContainsKey(vertexIndex)) {
										vertexIndices.Add(vertexIndex, vertices[i].Count);
										vertices[i].Add(LODMesh.vertices[vertexIndex] * subGridSize + RDGrid.ToLocal(SubGridPos + Vector3.one * (subGridSize - 1) / 2));
										uv[i].Add(uvOffset + new Vector2(LODMesh.uv[vertexIndex].x * uvScale.x, LODMesh.uv[vertexIndex].y * uvScale.y));
									}
								}

								foreach (int vertexIndex in LODMesh.triangles) {
									triangles[i].Add(vertexIndices[lodFace.vertexIndices[vertexIndex]]);
								}
							}
						}
					}
				}
			}
		}

		// create meshes
		for (int i = 0; i < lodCount; i++) {
			meshes[i] = new Mesh();
			meshes[i].vertices = vertices[i].ToArray();
			meshes[i].uv = uv[i].ToArray();
			meshes[i].triangles = triangles[i].ToArray();
			meshes[i].RecalculateNormals();
		}

		return meshes;
	}
}
