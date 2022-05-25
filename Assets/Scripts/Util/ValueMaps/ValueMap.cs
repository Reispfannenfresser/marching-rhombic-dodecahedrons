using UnityEngine;

namespace ValueMaps {
	public abstract class ValueMap2D {
		protected abstract float GetValue(float x, float y);

		public float this[float x, float y] {
			get {
				return GetValue(x, y);
			}
		}
	}
}
