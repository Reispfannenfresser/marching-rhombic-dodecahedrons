using System.IO;
using Newtonsoft.Json;
using System;
using UnityEngine;

#nullable enable
public static class JsonHelper {
	private static JsonSerializer serializer = new JsonSerializer();

	public static T? Deserialize<T>(string text) {
		try {
			return serializer.Deserialize<T>(new JsonTextReader(new StringReader(text)));
		}
		catch(Exception e) {
			Debug.LogError(e);
			return Deserialize<T>("{}");
		}
	}

	public static T? DeserializeFromFile<T>(string path) {
		try {
			return serializer.Deserialize<T>(new JsonTextReader(File.OpenText(path)));
		}
		catch(Exception e) {
			Debug.LogError(e);
			return Deserialize<T>("{}");
		}
	}
}
