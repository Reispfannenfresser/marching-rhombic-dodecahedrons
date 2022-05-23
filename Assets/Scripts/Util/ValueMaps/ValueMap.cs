using UnityEngine;

public abstract class ValueMap {
	protected abstract float GetValue(float x, float y);

	public float this[float x, float y] {
		get {
			return GetValue(x, y);
		}
	}
}
