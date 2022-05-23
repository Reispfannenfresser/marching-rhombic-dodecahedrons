using System;
using UnityEngine;

class PerlinNoise : ValueMap {
	protected readonly System.Random random;

	public PerlinNoise(int seed) {
		this.random = new System.Random(seed);
	}

	protected override float GetValue(float x, float y) {
		//TODO custom implementation that works differently for negative input parameters.
		return Mathf.PerlinNoise(x, y);
	}
}
