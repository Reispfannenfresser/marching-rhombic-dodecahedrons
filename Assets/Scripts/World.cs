using UnityEngine;
using System.Collections.Generic;

class Chunk {
	public readonly Vector3Int position;
	BlockState[,,] blocks;
	private readonly World world;

	public Mesh mesh {get; private set;} = null;
	private readonly int size;

	public Chunk(World world, Vector3Int position, int size) {
		this.size = size;
		this.world = world;
		this.position = position;
		this.blocks = new BlockState[size, size, size];
		for(int x = 0; x < size; x++) {
			for(int y = 0; y < size; y++) {
				for(int z = 0; z < size; z++) {
					blocks[x, y, z] = new BlockState(new Vector3Int(x, y, z), true);
				}
			}
		}

		mesh = new Mesh();
		RegenerateMesh();
	}

	private void RegenerateMesh() {
		mesh.Clear();

		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector3> normals = new List<Vector3>();
		for(int x = 0; x < size; x++) {
			for(int y = 0; y < size; y++) {
				for(int z = 0; z < size; z++) {
					Vector3Int blockPos = new Vector3Int(x, y, z);
					if (GetBlockState(blockPos).solid) {
						int offset = vertices.Count;
						foreach (Vector3 vertex in MeshData.vertices) {
							vertices.Add(World.GridToLocal(blockPos) + vertex);
						}
						foreach (int index in MeshData.triangles) {
							triangles.Add(offset + index);
						}
						foreach (Vector3 normal in MeshData.normals) {
							normals.Add(normal);
						}
					}
				}
			}
		}

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.normals = normals.ToArray();
	}

	public BlockState GetBlockState(Vector3Int blockPos) {
		return blocks[blockPos.x, blockPos.y, blockPos.z];
	}
}

class World : MonoBehaviour {
	[field: SerializeField]
	public int chunkSize {get;} = 10;
	[field: SerializeField]
	public int rendererCount {get;} = 1;

	Dictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();

	void Awake() {
		Chunk chunk = new Chunk(this, Vector3Int.zero, chunkSize);
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = chunk.mesh;
	}

	public static Vector3 GridToLocal(Vector3Int gridPos) {
		return gridPos.x * new Vector3(2, 0, 0) + gridPos.y * new Vector3(1, 1, 0) + gridPos.z * new Vector3(1, 0, 1);
	}

	public static Vector3Int LocalToGrid(Vector3 localPos) {
		Vector3 transformed = localPos.x * new Vector3(0.5f, 0, 0) + localPos.y * new Vector3(-0.5f, 1, 0) + localPos.z * new Vector3(-0.5f, 0, 1);
		return new Vector3Int((int) Mathf.Round(transformed.x), (int) Mathf.Round(transformed.y), (int) Mathf.Round(transformed.z));
	}

	public BlockState GetBlockState(Vector3Int position) {
		Vector3Int chunkPos = Vector3Int.zero;
		Vector3Int blockPos = Vector3Int.zero;
		for(int i = 0; i < 3; i++) {
			chunkPos[i] = position[i] / chunkSize;
			blockPos[i] = position[i] % chunkSize;
			if (position[i] < 0 && blockPos[i] != 0) {
				chunkPos[i] -= 1;
				blockPos[i] += chunkSize;
			}
		}

		if (chunks.ContainsKey(chunkPos)) {
			return chunks[chunkPos].GetBlockState(blockPos);
		}

		return null;
	}
}
