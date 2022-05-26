using System;
using System.Collections.Generic;

namespace ValueMaps {
	public class Noise<T> : ValueMap<int, T> {
		private Dictionary<int, T> values = new Dictionary<int, T>();
		public delegate T FromRandom(System.Random random);

		private FromRandom GenerateValue;

		private readonly System.Random pos;
		private readonly System.Random neg;
		private int max = -1;
		private int min = 0;

		public Noise(System.Random seedGenerator, FromRandom valueGenerator) : base(1) {
			this.pos = new System.Random(seedGenerator.Next());
			this.neg = new System.Random(seedGenerator.Next());
			this.GenerateValue = valueGenerator;
		}

		protected override T GetValue(int[] indices) {
			while (max < indices[0]) {
				values.Add(++max, GenerateValue(pos));
			}

			while (min > indices[0]) {
				values.Add(--min, GenerateValue(neg));
			}

			return values[indices[0]];
		}
	}

	public class Noise2D<T> : ValueMap<int, T> {
		private Noise<Noise<T>> values;

		public Noise2D(System.Random seedGenerator, Noise<T>.FromRandom valueGenerator) : base(2) {
			values = new Noise<Noise<T>>(seedGenerator, (random) => {
				return new Noise<T>(random, valueGenerator);
			});
		}

		protected override T GetValue(int[] indices) {
			return values[new int[] {indices[0]}][new int[] {indices[1]}];
		}
	}

	public class Noise3D<T> : ValueMap<int, T> {
		private Noise<Noise<Noise<T>>> values;

		public Noise3D(System.Random seedGenerator, Noise<T>.FromRandom valueGenerator) : base(3) {
			values = new Noise<Noise<Noise<T>>>(seedGenerator, (random) => {
				return new Noise<Noise<T>>(random, (random) => {
					return new Noise<T>(random, valueGenerator);
				});
			});
		}

		protected override T GetValue(int[] indices) {
			return values[new int[] {indices[0]}][new int[] {indices[1]}][new int[] {indices[2]}];
		}
	}
}
