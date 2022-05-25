using UnityEngine;

namespace ValueMaps {
	public class Flat2D : ValueMap2D {
		protected readonly float value;

		public Flat2D(float value) {
			this.value = value;
		}

		protected override float GetValue(float x, float y) {
			return value;
		}
	}
}
