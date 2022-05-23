using UnityEngine;

public class Transformed : ValueMap {
	protected readonly ValueMap valueMap;
	protected readonly Vector3 offset;
	protected readonly Vector3 scale;
	protected readonly float factor;

	public Transformed(ValueMap valueMap, Vector3 offset, Vector3 scale) {
		this.valueMap = valueMap;
		this.offset = offset;
		this.scale = scale;
	}

	protected override float GetValue(float x, float y) {
		return valueMap[x * scale.x + offset.x, y * scale.y + offset.y] * scale.z + offset.z;
	}
}
