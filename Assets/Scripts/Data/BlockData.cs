using UnityEngine;

namespace MRD.Data {
	public class BlockData {
		public readonly Vector3Int pos;
		public readonly Block block;

		public BlockData(Vector3Int pos, Block block) {
			this.pos = pos;
			this.block = block;
		}
	}
}
