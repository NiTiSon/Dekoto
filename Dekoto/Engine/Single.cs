using System.Collections;
using System.Collections.Generic;

namespace Dekoto.Engine;

public sealed class Single<T> : IEnumerable<T>
{
	public readonly T Value;
	public Single(T val)
	{
		Value = val;
	}

	public IEnumerator<T> GetEnumerator() => new Enumerator(this);
	IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

	private class Enumerator : IEnumerator<T>
	{
		private readonly T v;
		public Enumerator(Single<T> single)
		{
			v = single.Value;
		}
		public T Current => v;
		object IEnumerator.Current => v;

		public void Dispose() { }
		private bool moved;
		public bool MoveNext()
		{
			if (!moved)
			{
				moved = true;
				return true;
			}

			return false;
		}
		public void Reset()
		{
			moved = false;
		}
	}
}
