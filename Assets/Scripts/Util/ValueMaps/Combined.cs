using UnityEngine;

namespace ValueMaps {
	public class Combined2D : ValueMap2D {
		protected readonly ValueMap2D[] maps;

		public Combined2D(ValueMap2D[] maps) {
			this.maps = maps;
		}

		protected override float GetValue(float x, float y) {
			float value = 0;
			foreach(ValueMap2D map in maps) {
				value += map[x, y];
			}
			return value;
		}
	}
}
