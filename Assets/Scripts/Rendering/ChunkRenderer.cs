using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class ChunkRenderer : MonoBehaviour {
	public static ISet<Vector3Int?> dirtyRenderers = new HashSet<Vector3Int?>();
	public static Dictionary<Vector3Int?, ChunkRenderer> renderers = new Dictionary<Vector3Int?, ChunkRenderer>();

	private Mesh mesh {
		get {
			return meshFilter.mesh;
		}
		set {
			meshFilter.mesh = value;
		}
	}

	private MeshFilter meshFilter = null;
	public bool dirty {
		get {
			return dirtyRenderers.Contains(chunkData.chunkPosition);
		}
		set {
			if (dirty && !value) {
				mesh = ChunkMeshGenerator.GenerateMesh(chunkData);
				dirtyRenderers.Remove(chunkData.chunkPosition);
			}
			if (!dirty && value) {
				dirtyRenderers.Add(chunkData.chunkPosition);
			}
		}
	}

	private ChunkData _chunkData = null;
	public ChunkData chunkData {
		get {
			return _chunkData;
		}
		set {
			if (chunkData != null) {
				mesh.Clear();
				renderers.Remove(chunkData.position);

				if (dirtyRenderers.Contains(chunkData.position)) {
					dirtyRenderers.Remove(chunkData.position);
				}
			}

			_chunkData = value;

			if (chunkData != null) {
				transform.position = RDGrid.ToLocal(chunkData.position);
				renderers.Add(chunkData.chunkPosition, this);
				dirty = true;
			}

		}
	}

	private void Awake() {
		meshFilter = GetComponent<MeshFilter>();
	}

	private void OnDestroy() {
		chunkData = null;
	}
}