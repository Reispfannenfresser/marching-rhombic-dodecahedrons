using System;

namespace ValueMaps {
	public abstract class ValueMap<T, U> {
		protected readonly int dimensions;

		public ValueMap(int dimensions) {
			this.dimensions = dimensions;
		}

		protected abstract U GetValue(T[] indices);

		public U this[T[] indices] {
			get {
				if (indices.Length != dimensions) {
					throw new ArgumentException("Number of indices doesn't fit dimensions of valuemap.");
				}
				return GetValue(indices);
			}
		}
	}
}
