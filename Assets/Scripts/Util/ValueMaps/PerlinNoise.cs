using UnityEngine;
using System;
using System.Collections.Generic;

namespace ValueMaps {
	public class PerlinNoise2D : ValueMap2D {
		protected readonly System.Random random;
		private Dictionary<Vector2Int, Vector2> vectors = new Dictionary<Vector2Int, Vector2>();

		private RandomFloatAxis xAxis;
		private RandomFloatAxis yAxis;

		public PerlinNoise2D(System.Random seedGenerator) {
			xAxis = new RandomFloatAxis(seedGenerator);
			yAxis = new RandomFloatAxis(seedGenerator);
		}

		private Vector2 GetVector(Vector2Int gridPos) {
			if (!vectors.ContainsKey(gridPos)) {
				float angle = (xAxis[gridPos.x] + yAxis[gridPos.y]) * 2 * Mathf.PI;
				vectors.Add(gridPos, new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
			}
			return vectors[gridPos];
		}

		private float Fade(float value) {
			return 6 * value * value * value * value * value - 15 * value * value * value * value + 10 * value * value * value;
		}

		protected override float GetValue(float x, float y) {
			Vector2 pos = new Vector2(x, y);
			Vector2Int gridPos = new Vector2Int((int) Mathf.Floor(pos.x), (int) Mathf.Floor(pos.y));

			Vector2Int[] corners = {
				gridPos,
				gridPos + Vector2Int.up,
				gridPos + Vector2Int.one,
				gridPos + Vector2Int.right
			};

			float[] scalar = new float[4];
			for (int i = 0; i < 4; i++) {
				scalar[i] = Vector2.Dot(pos - corners[i], GetVector(corners[i]));
			}

			float weight1 = Fade(pos.x - corners[1].x);
			float weight2 = Fade(corners[1].y - pos.y);

			return Mathf.Lerp(Mathf.Lerp(scalar[1], scalar[2], weight1), Mathf.Lerp(scalar[0], scalar[3], weight1), weight2);
		}
	}
}
