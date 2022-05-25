using UnityEngine;

public class Combined : ValueMap {
	protected readonly ValueMap[] maps;

	public Combined(ValueMap[] maps) {
		this.maps = maps;
	}

	protected override float GetValue(float x, float y) {
		float value = 0;
		foreach(ValueMap map in maps) {
			value += map[x, y];
		}
		return value;
	}
}
