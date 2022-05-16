using UnityEngine;

public class BlockState {
	public readonly bool solid;
	public readonly Vector3Int position;

	public BlockState(Vector3Int position, bool solid) {
		this.position = position;
		this.solid = solid;
	}
}
