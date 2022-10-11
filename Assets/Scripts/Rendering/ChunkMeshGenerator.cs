using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using MRD.Data;

namespace MRD.Rendering
{
	public static class ChunkMeshGenerator
	{
		public static Mesh GenerateMesh(ChunkData chunkData)
		{
			List<Vector3> allVertices = new List<Vector3>();
			List<int> allTriangles = new List<int>();
			List<Vector2> allUV = new List<Vector2>();
			int triangleIndexOffset = 0;

			for (int x = 0; x < RDGrid.chunkSize; x++)
			{
				for (int y = 0; y < RDGrid.chunkSize; y++)
				{
					for (int z = 0; z < RDGrid.chunkSize; z++)
					{
						Vector3Int blockPos = new Vector3Int(x, y, z);

						Vector3[] vertices;
						int[] triangles;
						Vector2[] uv;

						MarchingShapes.rdMarcher.GetMeshForPos(chunkData, blockPos, out vertices, out triangles, out uv);

						triangleIndexOffset = allVertices.Count;
						allVertices.AddRange(vertices);
						allUV.AddRange(uv);
						foreach (int index in triangles)
						{
							allTriangles.Add(index + triangleIndexOffset);
						}
					}
				}
			}

			Mesh mesh = new Mesh();
			mesh.vertices = allVertices.ToArray();
			mesh.SetUVs(0, allUV);
			mesh.triangles = allTriangles.ToArray();
			mesh.RecalculateNormals();

			return mesh;
		}
	}
}
