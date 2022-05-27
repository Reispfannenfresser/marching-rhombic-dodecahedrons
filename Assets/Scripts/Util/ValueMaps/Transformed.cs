using UnityEngine;

namespace ValueMaps {
	public class Transformed<T, U, V, W> : ValueMap<T, W> {
		protected readonly ValueMap<U, V> valueMap;
		public delegate W TransformResult(V value);
		public delegate U[] TransformInput(T[] indices);
		private TransformResult transformResult;
		private TransformInput transformInput;

		public Transformed(int dimensions, ValueMap<U, V> valueMap, TransformInput inputTransformer, TransformResult resultTransformer) : base(dimensions) {
			this.valueMap = valueMap;
			this.transformResult = resultTransformer;
			this.transformInput = inputTransformer;
		}

		protected override W GetValue(T[] indices) {
			return transformResult(valueMap[transformInput(indices)]);
		}
	}
}
