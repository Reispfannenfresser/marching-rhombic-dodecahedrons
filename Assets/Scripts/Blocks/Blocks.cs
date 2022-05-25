using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Blocks {
	private static readonly Dictionary<string, Block> blocks = new Dictionary<string, Block>();

	public static void LoadBlocks() {
		string path = @"Data/Blocks";
		foreach (string filePath in Directory.EnumerateFiles(path)) {
			string name = Path.GetFileNameWithoutExtension(new FileInfo(filePath).Name);
			blocks[name] = new Block(name);
		}
	}

	public static Block GetBlock(string id) {
		if (!blocks.ContainsKey(id)) {
			blocks.Add(id, new Block(id));
		}

		return blocks[id];
	}
}
