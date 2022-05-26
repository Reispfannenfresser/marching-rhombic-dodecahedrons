using UnityEngine;

namespace ValueMaps {
	public class Combined<T, U> : ValueMap<T, U> {
		public delegate U Combine(U[] values);

		protected readonly ValueMap<T, U>[] maps;
		protected readonly Combine CombineValues;

		public Combined(int dimensions, ValueMap<T, U>[] maps, Combine valueCombiner) : base(dimensions) {
			this.maps = maps;
			this.CombineValues = valueCombiner;
		}

		protected override U GetValue(T[] indices) {
			U[] values = new U[maps.Length];
			for (int i = 0; i < maps.Length; i++) {
				values[i] = maps[i][indices];
			}
			return CombineValues(values);
		}
	}
}
