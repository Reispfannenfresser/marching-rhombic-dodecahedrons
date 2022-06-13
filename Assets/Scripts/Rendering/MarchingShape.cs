using UnityEngine;
using MeshData;
using System.Collections.Generic;
using MRD.Data;

namespace MRD.Rendering {
	public abstract class MarchingShape {
		public abstract void GetMeshForPos(ChunkData chunkData, Vector3Int position, out Vector3[] vertices, out int[] triangles, out Vector2[] uv);
	}

	public class CombinedMarchingShape : MarchingShape {
		private readonly MarchingShape[] shapes;

		public CombinedMarchingShape(MarchingShape[] shapes) {
			this.shapes = shapes;
		}

		public override void GetMeshForPos(ChunkData chunkData, Vector3Int position, out Vector3[] vertices, out int[] triangles, out Vector2[] uv) {
			List<Vector3> allVertices = new List<Vector3>();
			List<int> allTriangles = new List<int>();
			List<Vector2> allUV = new List<Vector2>();

			foreach (MarchingShape shape in shapes) {
				Vector3[] newVertices;
				int[] newTriangles;
				Vector2[] newUV;
				shape.GetMeshForPos(chunkData, position, out newVertices, out newTriangles, out newUV);

				int triangleIndexOffset = allVertices.Count;
				allVertices.AddRange(newVertices);
				allUV.AddRange(newUV);
				foreach(int index in newTriangles) {
					allTriangles.Add(index + triangleIndexOffset);
				}
			}

			vertices = allVertices.ToArray();
			triangles = allTriangles.ToArray();
			uv = allUV.ToArray();
		}
	}

	public class CustomMarchingShape : MarchingShape {
		private readonly Vector3Int[] gridNeighbors;
		private readonly Vector3 gridOffset;
		private readonly (Matrix4x4, int)[] transformedMeshes;
		private readonly MeshData.MeshData[] meshes;

		public CustomMarchingShape(Vector3Int[] gridNeighbors, Vector3 gridOffset, (Matrix4x4, int)[] transformedMeshes, MeshData.MeshData[] meshes) {
			this.gridNeighbors = gridNeighbors;
			this.gridOffset = gridOffset;
			this.transformedMeshes = transformedMeshes;
			this.meshes = meshes;
		}

		public override void GetMeshForPos(ChunkData chunkData, Vector3Int position, out Vector3[] vertices, out int[] triangles, out Vector2[] uv) {
			int meshIndex = 0;
			for (int i = 0; i < gridNeighbors.Length; i++) {
				BlockData neighbor = chunkData.blocks[position + gridNeighbors[i]];
				if (neighbor == null) {
					meshIndex = 0;
					break;
				}
				if (neighbor.block == Blocks.GetBlock("ground")) {
					meshIndex += 1 << i;
				}
			}

			(Matrix4x4 transformationMatrix, int tMeshIndex) = transformedMeshes[meshIndex];
			MeshData.MeshData meshData = meshes[tMeshIndex];

			vertices = new Vector3[meshData.vertices.Length];
			uv = new Vector2[meshData.vertices.Length];
			triangles = meshData.triangles;

			for (int i = 0; i < vertices.Length; i++) {
				VertexData vertex = meshData.vertices[i];

				vertices[i] = transformationMatrix.MultiplyVector(vertex.position) + RDGrid.ToLocal(position) + gridOffset;
				uv[i] = vertex.uv;
			}
		}
	}
}
