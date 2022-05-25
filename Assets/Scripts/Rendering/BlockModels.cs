using System.Collections.Generic;

public static class BlockModels {
	private static Dictionary<string, BlockModel> models = new Dictionary<string, BlockModel>();

	public static BlockModel GetBlockModel(string id) {
		if (!models.ContainsKey(id)) {
			models.Add(id, JsonHelper.DeserializeFromFile<BlockModel>(@"Data/Models/Blocks/" + id + ".json"));
		}
		return models[id];
	}
}
