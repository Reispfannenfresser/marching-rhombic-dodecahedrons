using UnityEngine;

namespace ValueMaps {
	public class Flat<T, U> : ValueMap<T, U> {
		protected readonly U value;

		public Flat(int dimensions, U value) : base(dimensions) {
			this.value = value;
		}

		protected override U GetValue(T[] indices) {
			return value;
		}
	}
}
