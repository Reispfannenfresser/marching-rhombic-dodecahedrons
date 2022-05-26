using UnityEngine;

namespace ValueMaps {
	public class Transformed : ValueMap<float, float> {
		protected readonly ValueMap<float, float> valueMap;
		protected readonly float scale;
		protected readonly float heightMultiplier;

		public Transformed(ValueMap<float, float> valueMap, float scale, float heightMultiplier) : base(1) {
			this.valueMap = valueMap;
			this.scale = scale;
			this.heightMultiplier = heightMultiplier;
		}

		protected override float GetValue(float[] indices) {
			return valueMap[new float[] {indices[0] * scale}] * heightMultiplier;
		}
	}

	public class Transformed2D : ValueMap<float, float> {
		protected readonly ValueMap<float, float> valueMap;
		protected readonly float x1;
		protected readonly float y1;
		protected readonly float x2;
		protected readonly float y2;
		protected readonly float heightMultiplier;

		public Transformed2D(ValueMap<float, float> valueMap, float scale, float angle, float heightMultiplier) :
			this(valueMap, Mathf.Cos(angle) * scale, Mathf.Sin(angle) * scale, -Mathf.Sin(angle) * scale, Mathf.Cos(angle) * scale, heightMultiplier) {}

		private Transformed2D(ValueMap<float, float> valueMap, float x1, float y1, float x2, float y2, float heightMultiplier) : base(2) {
			this.valueMap = valueMap;
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
			this.heightMultiplier = heightMultiplier;
		}

		protected override float GetValue(float[] indices) {
			return valueMap[new float[] {indices[0] * x1 + indices[1] * x2, indices[0] * y1 + indices[1] * y2}] * heightMultiplier;
		}
	}
}
