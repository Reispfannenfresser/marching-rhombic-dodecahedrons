using System;
using Newtonsoft.Json;

public struct Block {
	[System.Serializable]
	private struct BlockInformation {

	}

	public readonly string id;

	public Block(string id) {
		this.id = id;
		JsonHelper.DeserializeFromFile<BlockInformation>(@"Data/Blocks/" + id + ".json");
	}

	public static bool operator== (Block block1, Block block2) {
		return block1.id == block2.id;
	}

	public static bool operator!= (Block block1, Block block2) {
		return block1.id != block2.id;
	}

	public override bool Equals(Object obj) {
		return  obj is Block && (Block) obj == this;
	}

	public override int GetHashCode() {
		return id.GetHashCode();
	}
}
