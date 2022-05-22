using UnityEngine;

public static class MeshData {
	public static Vector3[][] vertices = {
		new Vector3[4] {
			//UL
			new Vector3(0, 1, 0),
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(-1, 0, 0)
		},
		new Vector3[4] {
			//UF
			new Vector3(0, 1, 0),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0, 0, 1)
		},
		new Vector3[4] {
			//UR
			new Vector3(0, 1, 0),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(1, 0, 0)
		},
		new Vector3[4] {
			//UB
			new Vector3(0, 1, 0),
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(0, 0, -1)
		},
		new Vector3[4] {
			//LF
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(-1, 0, 0),
			new Vector3(0, 0, 1),
			new Vector3(-0.5f, -0.5f, 0.5f)
		},
		new Vector3[4] {
			//FR
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0, 0, 1),
			new Vector3(1, 0, 0),
			new Vector3(0.5f, -0.5f, 0.5f)
		},
		new Vector3[4] {
			//RB
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(1, 0, 0),
			new Vector3(0, 0, -1),
			new Vector3(0.5f, -0.5f, -0.5f)
		},
		new Vector3[4] {
			//BL
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(0, 0, -1),
			new Vector3(-1, 0, 0),
			new Vector3(-0.5f, -0.5f, -0.5f)
		},
		new Vector3[4] {
			//DL
			new Vector3(-1, 0, 0),
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(0, -1, 0)
		},
		new Vector3[4] {
			//DF
			new Vector3(0, 0, 1),
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, 0.5f),
			new Vector3(0, -1, 0)
		},
		new Vector3[4] {
			//DR
			new Vector3(1, 0, 0),
			new Vector3(0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, -0.5f),
			new Vector3(0, -1, 0)
		},
		new Vector3[4] {
			//DB
			new Vector3(0, 0, -1),
			new Vector3(0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(0, -1, 0)
		}
	};

	public static int[] triangles = {
		0, 1, 2,
		3, 2, 1
	};

	public static Vector3[] normals = {
		//UL
		new Vector3(-1, 1, 0),
		//UF
		new Vector3(0, 1, 1),
		//UR
		new Vector3(1, 1, 0),
		//UB
		new Vector3(0, 1, -1),
		//LF
		new Vector3(-1, 0, 1),
		//FR
		new Vector3(1, 0, 1),
		//RB
		new Vector3(1, 0, -1),
		//BL
		new Vector3(-1, 0, -1),
		//DL
		new Vector3(-1, -1, 0),
		//DF
		new Vector3(0, -1, 1),
		//DR
		new Vector3(1, -1, 0),
		//DB
		new Vector3(0, -1, -1),
	};
}
