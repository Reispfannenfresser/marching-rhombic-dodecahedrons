using UnityEngine;
using System.Collections.Generic;
using System;

class Chunk {
	public readonly Vector3Int position;
	BlockState[,,] blocks;
	public Mesh mesh {get; private set;} = null;

	private readonly World world;

	public Chunk(World world, Vector3Int position) {
		this.world = world;
		this.position = position;
		this.blocks = new BlockState[world.chunkSize.x, world.chunkSize.y, world.chunkSize.z];
		for(int x = 0; x < world.chunkSize.x; x++) {
			for(int y = 0; y < world.chunkSize.y; y++) {
				for(int z = 0; z < world.chunkSize.z; z++) {
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
		for(int x = 0; x < world.chunkSize.x; x++) {
			for(int y = 0; y < world.chunkSize.y; y++) {
				for(int z = 0; z < world.chunkSize.z; z++) {
					Vector3Int blockPos = new Vector3Int(x, y, z);
					if (GetBlockState(blockPos).solid) {
						int offset = vertices.Count;
						foreach (FaceDirection direction in Enum.GetValues(typeof(FaceDirection))) {
							BlockState neighbor = GetBlockState(blockPos + direction.GetVector());
							if (neighbor == null || !neighbor.solid) {
								int triangleIndexOffset = vertices.Count;
								foreach(Vector3 vertex in MeshData.vertices[(int) direction]) {
									vertices.Add(vertex + World.GridToLocal(blockPos));
									normals.Add(MeshData.normals[(int) direction]);
								}
								foreach(int triangleIndex in MeshData.triangles) {
									triangles.Add(triangleIndex + triangleIndexOffset);
								}
							}
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
		if (blockPos.x >= 0 && blockPos.x < world.chunkSize.x && blockPos.y >= 0 && blockPos.y < world.chunkSize.z && blockPos.z >= 0 && blockPos.z < world.chunkSize.z) {
			return blocks[blockPos.x, blockPos.y, blockPos.z];
		}
		return world.GetBlockState(new Vector3Int(position.x * world.chunkSize.x, position.y * world.chunkSize.y, position.z * world.chunkSize.z) + blockPos);
	}
}
