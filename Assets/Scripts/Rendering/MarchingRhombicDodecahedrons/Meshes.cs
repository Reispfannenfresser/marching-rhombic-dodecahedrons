using UnityEngine;
using MeshData;

namespace MarchingRhombicDodecahedrons {
	public class Meshes {
		private struct TransformedMesh {
			Matrix4x4 transformationMatrix;
			int meshIndex;
			public TransformedMesh(Matrix4x4 transformationMatrix, int meshIndex) {
				this.transformationMatrix = transformationMatrix;
				this.meshIndex = meshIndex;
			}
		}

		private static MeshData.MeshData[] tetrahedronMeshes = new MeshData.MeshData[] {
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
					new VertexData(new Vector3(0.5f, 0, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, 0, 0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, 0, -0.5f), new Vector2(0, 0))
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
		};

		private static MeshData.MeshData[] octahedronMeshes = new MeshData.MeshData[] {
			// 0 or 6
			new MeshData.MeshData(
				new VertexData[0],
				new int[0]
			),
			// 1
			new MeshData.MeshData(
				new VertexData[] {
					new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, 0.5f, -0.5f), new Vector2(0, 0))
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
					new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0))
				},
				new int[] {
					0, 1, 2,
					3, 4, 5,
					5, 6, 3,
					7, 8, 9
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
					new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, -0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, -0.5f, 0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, -0.5f, 0), new Vector2(0, 0))

				},
				new int[] {
					0, 1, 2,
					3, 4, 5,
					5, 6, 3,
					7, 8, 9,
					9, 10, 7,
					11, 12, 13
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
					0, 3, 4,
					0, 4, 5
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
					0, 1, 2,
					0, 2, 3,
					0, 3, 4,
					0, 4, 5
				}
			),
			// 4
			new MeshData.MeshData(
				new VertexData[] {
					new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, 0, -0.5f), new Vector2(0, 0))
				},
				new int[] {
					0, 1, 2,
					3, 4, 5,
					5, 6, 3,
					7, 8, 9
				}
			),
			new MeshData.MeshData(
				new VertexData[] {
					new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, 0.5f, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(0.5f, -0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, -0.5f, 0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, -0.5f, -0.5f), new Vector2(0, 0))
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
					new VertexData(new Vector3(0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, 0.5f, -0.5f), new Vector2(0, 0)),
					new VertexData(new Vector3(-0.5f, 0.5f, 0), new Vector2(0, 0)),
					new VertexData(new Vector3(0, 0.5f, 0.5f), new Vector2(0, 0))
				},
				new int[] {
					0, 1, 2,
					2, 3, 0
				}
			)
		};

		// 0001: BLU
		// 0010: FRU
		// 0100: FLD
		// 1000: BRD
		private static TransformedMesh[] transformedTetrahedronMeshes = new TransformedMesh[] {
			// 0000
			new TransformedMesh(Matrix4x4.identity, 0),
			// 0001
			new TransformedMesh(Matrix4x4.identity, 1),
			// 0010
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 1),
			// 0011
			new TransformedMesh(Matrix4x4.identity, 2),
			// 0100
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 1),
			// 0101
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 2),
			// 0110
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 2),
			// 0111
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 180)), 3),
			// 1000
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 180)), 1),
			// 1001
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 2),
			// 1010
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90)), 2),
			// 1011
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 3),
			// 1100
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 2),
			// 1101
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 3),
			// 1110
			new TransformedMesh(Matrix4x4.identity, 3),
			// 1111
			new TransformedMesh(Matrix4x4.identity, 0),
		};

		// 000001: U
		// 000010: B
		// 000100: L
		// 001000: F
		// 010000: R
		// 100000: D
		private static TransformedMesh[] transformedOctahedronMeshes = new TransformedMesh[] {
			// 000000
			new TransformedMesh(Matrix4x4.identity, 0),
			// 000001
			new TransformedMesh(Matrix4x4.identity, 1),
			// 000010
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 1),
			// 000011
			new TransformedMesh(Matrix4x4.identity, 2),
			// 000100
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 1),
			// 000101
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 2),
			// 000110
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 2),
			// 000111
			new TransformedMesh(Matrix4x4.identity, 5),
			// 001000
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 1),
			// 001001
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 2),
			// 001010
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 3),
			// 001011
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 4),
			// 001100
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 90)), 2),
			// 001101
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 5),
			// 001110
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 90)), 4),
			// 001111
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(-90, -90, 0)), 7),
			// 010000
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90)), 1),
			// 010001
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 2),
			// 010010
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90)), 2),
			// 010011
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 5),
			// 010100
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 3),
			// 010101
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 90, 0)), 4),
			// 010110
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 4),
			// 010111
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 7),
			// 011000
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, -90)), 2),
			// 011001
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 5),
			// 011010
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, -90)), 4),
			// 011011
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 7),
			// 011100
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 90)), 4),
			// 011101
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 7),
			// 011110
			new TransformedMesh(Matrix4x4.identity, 8),
			// 011111
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 9),
			// 100000
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(180, 0, 0)), 1),
			// 100001
			new TransformedMesh(Matrix4x4.identity, 3),
			// 100010
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 2),
			// 100011
			new TransformedMesh(Matrix4x4.identity, 4),
			// 100100
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(-90, 90, 0)), 2),
			// 100101
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 4),
			// 100110
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 6),
			// 100111
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, -90)), 7),
			// 101000
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(-90, 180, 0)), 2),
			// 101001
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 4),
			// 101010
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 4),
			// 101011
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 8),
			// 101100
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 6),
			// 101101
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90)), 7),
			// 101110
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 7),
			// 101111
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90)), 9),
			// 110000
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(-90, -90, 0)), 2),
			// 110001
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0)), 4),
			// 110010
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 6),
			// 110011
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 90)), 7),
			// 110100
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 90)), 4),
			// 110101
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 8),
			// 110110
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 180, 0)), 7),
			// 110111
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)), 9),
			// 111000
			new TransformedMesh(Matrix4x4.identity, 6),
			// 111001
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 7),
			// 111010
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)), 7),
			// 111011
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), 9),
			// 111100
			new TransformedMesh(Matrix4x4.identity, 7),
			// 111101
			new TransformedMesh(Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0)), 9),
			// 111110
			new TransformedMesh(Matrix4x4.identity, 9),
			// 111111
			new TransformedMesh(Matrix4x4.identity, 0),
		};
	}
}
