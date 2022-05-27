using System.Collections.Generic;

public static class BlockModels {
	private static Dictionary<string, BlockModelData> models = new Dictionary<string, BlockModelData>();

	public static BlockModelData GetBlockModel(string id) {
		if (!models.ContainsKey(id)) {
			models.Add(id, JsonHelper.DeserializeFromFile<BlockModelData>(@"Data/Models/Blocks/" + id + ".json"));
		}
		return models[id];
	}
}
