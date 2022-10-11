using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Blocks
{
	private static readonly Dictionary<string, Block> blocks = new Dictionary<string, Block>();

	public static readonly Block GROUND = new Block("ground", false);
	public static readonly Block INDESTRUCTIBLE = new Block("indestructible", true);

	public static void RegisterBlock(Block block)
	{
		blocks[block.id] = block;
	}

	public static Block GetBlock(string id)
	{
		if (!blocks.ContainsKey(id))
		{
			throw new System.ArgumentException("A block with that ID doesn't exist");
		}

		return blocks[id];
	}
}
