using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using MeshData;
using MarchingRhombicDodecahedrons;

public static class ChunkMeshGenerator {
	public static Mesh GenerateMesh(ChunkData chunkData) {
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector2> uv = new List<Vector2>();

		for(int x = 0; x < RDGrid.chunkSize; x++) {
			for(int y = 0; y < RDGrid.chunkSize; y++) {
				for(int z = 0; z < RDGrid.chunkSize; z++) {
					Vector3Int blockPos = new Vector3Int(x, y, z);

					// octahedron
					bool shouldSkip = false;
					int meshIndex = 0;
					for (int i = 0; i < Meshes.octahedronGrid.Length; i++) {
						BlockData neighbor = chunkData.blocks[blockPos + Meshes.octahedronGrid[i]];
						if (neighbor == null) {
							shouldSkip = true;
							break;
						}
						if (neighbor.block == Blocks.GetBlock("ground")) {
							meshIndex += 1 << i;
						}
					}

					if (!shouldSkip) {
						Meshes.TransformedMesh transformedMesh = Meshes.transformedOctahedronMeshes[meshIndex];
						MeshData.MeshData meshData = Meshes.octahedronMeshes[transformedMesh.meshIndex];

						int triangleIndexOffset = vertices.Count;

						foreach(VertexData vertex in meshData.vertices) {
							vertices.Add(transformedMesh.transformationMatrix.MultiplyVector(vertex.position) + RDGrid.ToLocal(blockPos) + Meshes.octahedronGridOffset);
							uv.Add(vertex.uv);
						}
						foreach(int triangleIndex in meshData.triangles) {
							triangles.Add(triangleIndex + triangleIndexOffset);
						}
					}


					// tetrahedron1
					shouldSkip = false;
					meshIndex = 0;
					for (int i = 0; i < Meshes.tetrahedronGrid.Length; i++) {
						BlockData neighbor = chunkData.blocks[blockPos + Meshes.tetrahedronGrid[i]];
						if (neighbor == null) {
							shouldSkip = true;
							break;
						}
						if (neighbor.block == Blocks.GetBlock("ground")) {
							meshIndex += 1 << i;
						}
					}

					if (!shouldSkip) {
						Meshes.TransformedMesh transformedMesh = Meshes.transformedTetrahedronMeshes[meshIndex];
						MeshData.MeshData meshData = Meshes.tetrahedronMeshes[transformedMesh.meshIndex];

						int triangleIndexOffset = vertices.Count;

						foreach(VertexData vertex in meshData.vertices) {
							vertices.Add(transformedMesh.transformationMatrix.MultiplyVector(vertex.position) + RDGrid.ToLocal(blockPos) + Meshes.tetrahedronGridOffset);
							uv.Add(vertex.uv);
						}
						foreach(int triangleIndex in meshData.triangles) {
							triangles.Add(triangleIndex + triangleIndexOffset);
						}
					}

					// tetrahedron2
					shouldSkip = false;
					meshIndex = 0;
					for (int i = 0; i < Meshes.tetrahedronGrid2.Length; i++) {
						BlockData neighbor = chunkData.blocks[blockPos + Meshes.tetrahedronGrid2[i]];
						if (neighbor == null) {
							shouldSkip = true;
							break;
						}
						if (neighbor.block == Blocks.GetBlock("ground")) {
							meshIndex += 1 << i;
						}
					}

					if (!shouldSkip) {
						Meshes.TransformedMesh transformedMesh = Meshes.transformedTetrahedronMeshes[meshIndex];
						MeshData.MeshData meshData = Meshes.tetrahedronMeshes[transformedMesh.meshIndex];

						int triangleIndexOffset = vertices.Count;

						foreach(VertexData vertex in meshData.vertices) {
							vertices.Add(Meshes.tetrahedronGrid2Transform.MultiplyVector(transformedMesh.transformationMatrix.MultiplyVector(vertex.position)) + RDGrid.ToLocal(blockPos) + Meshes.tetrahedronGrid2Offset);
							uv.Add(vertex.uv);
						}
						foreach(int triangleIndex in meshData.triangles) {
							triangles.Add(triangleIndex + triangleIndexOffset);
						}
					}
				}
			}
		}

		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.uv = uv.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();

		return mesh;
	}
}
