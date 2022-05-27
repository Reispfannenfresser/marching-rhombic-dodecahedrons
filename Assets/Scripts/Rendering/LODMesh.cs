using UnityEngine;

public static class LODMesh {
	public static readonly Vector3[] vertices = new Vector3[] {
		new Vector3(-0.5f, 0.5f, 0),
		new Vector3(0.5f, 0.5f, 1),
		new Vector3(1.5f, 0.5f, 0),
		new Vector3(0.5f, 0.5f, -1),
		new Vector3(-1.5f, -0.5f, 0),
		new Vector3(-0.5f, -0.5f, 1),
		new Vector3(0.5f, -0.5f, 0),
		new Vector3(-0.5f, -0.5f, -1),
		new Vector3(1.5f, 0.5f, 0),
		new Vector3(0.5f, 0.5f, -1),
		new Vector3(0.5f, -0.5f, 0),
		new Vector3(-0.5f, -0.5f, -1)
	};

	public static readonly Vector2[] uv = new Vector2[] {
		new Vector2(0, 0),
		new Vector2(1, 0),
		new Vector2(0, 0),
		new Vector2(1, 0),
		new Vector2(0, 1),
		new Vector2(1, 1),
		new Vector2(0, 1),
		new Vector2(1, 1),
		new Vector2(1, 1),
		new Vector2(0, 1),
		new Vector2(1, 0),
		new Vector2(0, 0)
	};

	public struct LODFace {
		public readonly FaceDirection culledAs;
		public int[] vertexIndices;

		public LODFace(FaceDirection culledAs, int[] vertexIndices) {
			this.culledAs = culledAs;
			this.vertexIndices = vertexIndices;
		}
	}

	public static LODFace[] faces = {
		new LODFace(FaceDirection.UR, new int[] {
			0, 1, 8, 9
		}),
		new LODFace(FaceDirection.LF, new int[] {
			5, 1, 0, 4
		}),
		new LODFace(FaceDirection.FR, new int[] {
			6, 2, 1, 5
		}),
		new LODFace(FaceDirection.RB, new int[] {
			7, 3, 2, 6
		}),
		new LODFace(FaceDirection.BL, new int[] {
			4, 0, 3, 7
		}),
		new LODFace(FaceDirection.DL, new int[] {
			4, 11, 10, 5
		}),
	};

	public static readonly int[] triangles = new int[] {
		0, 1, 2,
		2, 3, 0
	};
}
