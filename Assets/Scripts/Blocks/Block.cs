using System;
using Newtonsoft.Json;

public struct Block
{
	public readonly string id;

	public readonly bool visible;

	public readonly bool indestructible;

	public Block(string id, bool visible, bool indestructible = false)
	{
		this.id = id;
		this.indestructible = indestructible;
		this.visible = visible;
		Blocks.RegisterBlock(this);
	}

	public static bool operator ==(Block block1, Block block2)
	{
		return block1.id == block2.id;
	}

	public static bool operator !=(Block block1, Block block2)
	{
		return block1.id != block2.id;
	}

	public override bool Equals(Object obj)
	{
		return obj is Block && (Block)obj == this;
	}

	public override int GetHashCode()
	{
		return id.GetHashCode();
	}
}
