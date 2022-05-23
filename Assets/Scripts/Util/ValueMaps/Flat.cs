using UnityEngine;

public class Flat : ValueMap {
	protected readonly float value;

	public Flat(float value) {
		this.value = value;
	}

	protected override float GetValue(float x, float y) {
		return value;
	}
}
