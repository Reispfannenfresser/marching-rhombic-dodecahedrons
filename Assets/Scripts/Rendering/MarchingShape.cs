using UnityEngine;
using MeshData;
using System.Collections.Generic;
using MRD.Data;

namespace MRD.Rendering {
	public class MarchingShape {
		private readonly Vector3Int[] gridNeighbors;
		private readonly Vector3 gridOffset;
		private readonly (Matrix4x4, int)[] transformedMeshes;
		private readonly MeshData.MeshData[] meshes;

		private MarchingShape(Vector3Int[] gridNeighbors, Vector3 gridOffset, (Matrix4x4, int)[] transformedMeshes, MeshData.MeshData[] meshes) {
			this.gridNeighbors = gridNeighbors;
			this.gridOffset = gridOffset;
			this.transformedMeshes = transformedMeshes;
			this.meshes = meshes;
		}

		public void GetMeshForPos(ChunkData chunkData, Vector3Int position, out Vector3[] vertices, out int[] triangles, out Vector2[] uv) {
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

		public static MarchingShape marchingTetrahedron1 = new MarchingShape(
			new Vector3Int[] {
				Vector3Int.zero,
				FaceDirection.FR.GetVector(),
				FaceDirection.DF.GetVector(),
				FaceDirection.DR.GetVector()
			},
			new Vector3(0.5f, -0.5f, 0.5f),
			new (Matrix4x4, int)[] {
				// 0000
				(Matrix4x4.identity, 0),
				// 0001
				(Matrix4x4.identity, 1),
				// 0010
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 1),
				// 0011
				(Matrix4x4.identity, 2),
				// 0100
				(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 1),
				// 0101
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 2),
				// 0110
				(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 2),
				// 0111
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 180)), 3),
				// 1000
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 180)), 1),
				// 1001
				(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 2),
				// 1010
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90)), 2),
				// 1011
				(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 3),
				// 1100
				(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 2),
				// 1101
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 3),
				// 1110
				(Matrix4x4.identity, 3),
				// 1111
				(Matrix4x4.identity, 0),
			},
			new MeshData.MeshData[] {
				// 0 or 4
				new MeshData.MeshData(
					new VertexData[0],
					new int[0]
				),
				// 1
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0, -0.5f), new Vector2(0, 0))
					},
					new int[] {
						0, 1, 2
					}
				),
				// 2
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, 0, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0, 0), new Vector2(0, 0))
					},
					new int[] {
						0, 1, 2,
						2, 3, 0
					}
				),
				// 3
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0, 0), new Vector2(0, 0))
					},
					new int[] {
						0, 1, 2
					}
				)
			});

		public static MarchingShape marchingTetrahedron2 = new MarchingShape(
			new Vector3Int[] {
				Vector3Int.zero,
				FaceDirection.LF.GetVector(),
				FaceDirection.DL.GetVector(),
				FaceDirection.DF.GetVector()
			},
			new Vector3(-0.5f, -0.5f, 0.5f),
			new (Matrix4x4, int)[] {
				// 0000
				(Matrix4x4.identity, 0),
				// 0001
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 1),
				// 0010
				(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 1),
				// 0011
				(Matrix4x4.identity, 2),
				// 0100
				(Matrix4x4.Rotate(Quaternion.Euler(180, -90, 0)), 1),
				// 0101
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 90)), 2),
				// 0110
				(Matrix4x4.Rotate(Quaternion.Euler(90, -90, 0)), 2),
				// 0111
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 180)), 3),
				// 1000
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 180)), 1),
				// 1001
				(Matrix4x4.Rotate(Quaternion.Euler(-90, -90, 0)), 2),
				// 1010
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, -90)), 2),
				// 1011
				(Matrix4x4.Rotate(Quaternion.Euler(180, -90, 0)), 3),
				// 1100
				(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 2),
				// 1101
				(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 3),
				// 1110
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 3),
				// 1111
				(Matrix4x4.identity, 0),
			},
			marchingTetrahedron1.meshes
			);

		public static MarchingShape marchingOctahedron = new MarchingShape(
			new Vector3Int[] {
				Vector3Int.zero,
				FaceDirection.DB.GetVector(),
				FaceDirection.DL.GetVector(),
				FaceDirection.DF.GetVector(),
				FaceDirection.DR.GetVector(),
				CornerDirection.D.GetVector()
			},
			new Vector3(0, -1f, 0),
			new (Matrix4x4, int)[] {
				// 000000
				(Matrix4x4.identity, 0),
				// 000001
				(Matrix4x4.identity, 1),
				// 000010
				(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 1),
				// 000011
				(Matrix4x4.identity, 2),
				// 000100
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 1),
				// 000101
				(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 2),
				// 000110
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 2),
				// 000111
				(Matrix4x4.identity, 5),
				// 001000
				(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 1),
				// 001001
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 2),
				// 001010
				(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 3),
				// 001011
				(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 4),
				// 001100
				(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 90)), 2),
				// 001101
				(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 5),
				// 001110
				(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 90)), 4),
				// 001111
				(Matrix4x4.Rotate(Quaternion.Euler(-90, -90, 0)), 7),
				// 010000
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90)), 1),
				// 010001
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 2),
				// 010010
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90)), 2),
				// 010011
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 5),
				// 010100
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 3),
				// 010101
				(Matrix4x4.Rotate(Quaternion.Euler(90, 90, 0)), 4),
				// 010110
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 4),
				// 010111
				(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 7),
				// 011000
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 90)), 2),
				// 011001
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 5),
				// 011010
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 90)), 4),
				// 011011
				(Matrix4x4.Rotate(Quaternion.Euler(-90, 90, 0)), 7),
				// 011100
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 90)), 4),
				// 011101
				(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 7),
				// 011110
				(Matrix4x4.identity, 8),
				// 011111
				(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 9),
				// 100000
				(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 1),
				// 100001
				(Matrix4x4.identity, 3),
				// 100010
				(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 2),
				// 100011
				(Matrix4x4.identity, 4),
				// 100100
				(Matrix4x4.Rotate(Quaternion.Euler(-90, 90, 0)), 2),
				// 100101
				(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 4),
				// 100110
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 6),
				// 100111
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 90)), 7),
				// 101000
				(Matrix4x4.Rotate(Quaternion.Euler(-90, 180, 0)), 2),
				// 101001
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 4),
				// 101010
				(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 4),
				// 101011
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 8),
				// 101100
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 6),
				// 101101
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90)), 7),
				// 101110
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 7),
				// 101111
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90)), 9),
				// 110000
				(Matrix4x4.Rotate(Quaternion.Euler(-90, -90, 0)), 2),
				// 110001
				(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 4),
				// 110010
				(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 6),
				// 110011
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, -90)), 7),
				// 110100
				(Matrix4x4.Rotate(Quaternion.Euler(-90, 90, 0)), 4),
				// 110101
				(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 8),
				// 110110
				(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 7),
				// 110111
				(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 9),
				// 111000
				(Matrix4x4.identity, 6),
				// 111001
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 7),
				// 111010
				(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 7),
				// 111011
				(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 9),
				// 111100
				(Matrix4x4.identity, 7),
				// 111101
				(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 9),
				// 111110
				(Matrix4x4.identity, 9),
				// 111111
				(Matrix4x4.identity, 0),
			},
			new MeshData.MeshData[] {
				// 0 or 6
				new MeshData.MeshData(
					new VertexData[0],
					new int[0]
				),
				// 1
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0))
					},
					new int[] {
						0, 1, 2,
						2, 3, 0
					}
				),
				// 2
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0)),
					},
					new int[] {
						0, 1, 2,
						2, 3, 0,
						4, 5, 6,
						6, 7, 4
					}
				),
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, -0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, 0.5f), new Vector2(0, 0))
					},
					new int[] {
						0, 1, 2,
						2, 3, 0,
						4, 5, 6,
						6, 7, 4
					}
				),
				// 3
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, -0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0, 0), new Vector2(0, 0))
					},
					new int[] {
						0, 1, 2,
						2, 3, 0,
						4, 5, 6,
						6, 7, 4,
						8, 9, 10,
						10, 11, 8,
						12, 13, 14,
						14, 15, 12
					}
				),
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
					},
					new int[] {
						0, 1, 2,
						0, 2, 3,
						0, 3, 5,
						3, 4, 5
					}
				),
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0, 0.5f), new Vector2(0, 0)),
					},
					new int[] {
						0, 4, 5,
						0, 3, 4,
						1, 2, 3,
						0, 1, 3
					}
				),
				// 4
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
					},
					new int[] {
						0, 1, 2,
						2, 3, 0,
						4, 5, 6,
						6, 7, 4
					}
				),
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, -0.5f, 0), new Vector2(0, 0))
					},
					new int[] {
						0, 1, 2,
						2, 3, 0,
						4, 5, 6,
						6, 7, 4
					}
				),
				// 5
				new MeshData.MeshData(
					new VertexData[] {
						new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
						new VertexData(new Vector3(0, 0.5f, -0.5f), new Vector2(0, 0)),
						new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0))
					},
					new int[] {
						0, 1, 2,
						2, 3, 0
					}
				)
			});
	}
}
