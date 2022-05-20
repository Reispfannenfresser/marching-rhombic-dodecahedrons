using UnityEngine;

public class BlockData {
	public readonly bool solid;
	public readonly Vector3Int position;

	public BlockData(Vector3Int position, bool solid) {
		this.position = position;
		this.solid = solid;
	}
}
