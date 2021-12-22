namespace GameNameSpace
{
	public class CircularArray<T>
	{
		public int CurrentStart { get; protected set; }
		public int MaxCapacity { get; protected set; }
		protected T[] Values;

		public CircularArray(int maxCapacity)
		{
			CurrentStart = 0;
			MaxCapacity = maxCapacity;
			Values = new T[MaxCapacity];
		}

		public T Get(int startOffset)
		{
			startOffset %= MaxCapacity;
			if ((CurrentStart - startOffset) < 0)
			{
				return (Values[(CurrentStart + (MaxCapacity - startOffset)) % MaxCapacity]);
			}
			return (Values[(CurrentStart - startOffset) % MaxCapacity]);
		}

		public void Set(T newValue)
		{
			Values[CurrentStart++] = newValue;
			CurrentStart %= MaxCapacity;
		}
	}
}