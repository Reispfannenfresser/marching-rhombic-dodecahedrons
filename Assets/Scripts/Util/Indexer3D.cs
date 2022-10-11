using UnityEngine;

public abstract class Indexer3D<S, T>
{
	protected readonly S obj;

	public Indexer3D(S obj)
	{
		this.obj = obj;
	}

	protected abstract void Set(Vector3Int position, T value);
	protected abstract T Get(Vector3Int position);

	public T this[Vector3Int position]
	{
		get
		{
			return Get(position);
		}
		set
		{
			Set(position, value);
		}
	}
}
