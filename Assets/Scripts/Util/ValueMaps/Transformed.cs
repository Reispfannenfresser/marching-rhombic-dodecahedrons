using UnityEngine;

public class Transformed : ValueMap {
	protected readonly ValueMap valueMap;
	protected readonly float x1;
	protected readonly float y1;
	protected readonly float x2;
	protected readonly float y2;
	protected readonly float heightMultiplier;

	public Transformed(ValueMap valueMap, float scale, float angle, float heightMultiplier) :
		this(valueMap, Mathf.Cos(angle) * scale, Mathf.Sin(angle) * scale, -Mathf.Sin(angle) * scale, Mathf.Cos(angle) * scale, heightMultiplier) {}


	public Transformed(ValueMap valueMap, float x1, float y1, float x2, float y2, float heightMultiplier) {
		this.valueMap = valueMap;
		this.x1 = x1;
		this.y1 = y1;
		this.x2 = x2;
		this.y2 = y2;
		this.heightMultiplier = heightMultiplier;
	}

	protected override float GetValue(float x, float y) {
		return valueMap[x * x1 + y * x2, x * y1 + y * y2] * heightMultiplier;
	}
}
