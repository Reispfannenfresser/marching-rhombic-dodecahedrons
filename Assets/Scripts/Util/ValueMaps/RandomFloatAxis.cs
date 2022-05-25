using System.Collections.Generic;
using System;

public class RandomFloatAxis {
	private Dictionary<int, float> values = new Dictionary<int, float>();

	private readonly System.Random pos;
	private readonly System.Random neg;
	private int max = -1;
	private int min = 0;

	public RandomFloatAxis(System.Random seedGenerator) {
		pos = new System.Random(seedGenerator.Next());
		neg = new System.Random(seedGenerator.Next());
	}

	public float this[int index] {
		get {
			if (!values.ContainsKey(index)) {
				if (index >= 0) {
					while (max < index) {
						values.Add(++max, (float) pos.NextDouble());
					}
				} else {
					while (min > index) {
						values.Add(--min, (float) neg.NextDouble());
					}
				}
			}

			return values[index];
		}
	}
}
