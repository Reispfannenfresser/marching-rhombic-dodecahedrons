using System;
using UnityEngine;
using Newtonsoft.Json;

namespace MeshData {
	[System.Serializable]
	public struct VertexData {
		public readonly Vector3 position;
		public readonly Vector2 uv;

		[JsonConstructor]
		public VertexData(float[] position, float[] uv) {
			this.position = (position == null || position.Length != 3) ? default(Vector3) : new Vector3(position[0], position[1], position[2]);
			this.uv = (uv == null || uv.Length != 2) ? default(Vector2) : new Vector2(uv[0], uv[1]);
		}

		public VertexData(Vector3 position, Vector2 uv) {
			this.position = position;
			this.uv = uv;
		}
	}

	[System.Serializable]
	public struct MeshData {
		public readonly VertexData[] vertices;
		public readonly int[] triangles;

		[JsonConstructor]
		public MeshData(VertexData[] vertices, int[] triangles) {
			this.vertices = (vertices == null) ? new VertexData[0] : vertices;
			this.triangles = (triangles == null) ? new int[0] : triangles;
		}
	}
}
