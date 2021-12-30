using System.Collections.Generic;
using System.Diagnostics;

namespace GameNameSpace
{
	public class ObjectPool<T> where T : class, new()
	{
		protected T[] GameObjects;
		protected int CurrentIndex;
		public int Capacity { get; protected set; }
		
		public ObjectPool(int capacity)
		{
			Capacity = capacity;
			GameObjects = new T[Capacity];
			for (int i = 0; i < Capacity; i++)
			{
				T newObject = new T();
				GameObjects[i] = newObject;
				(newObject as IGameObject).Status = GameObjectStatus.OUTDATED;
			}
			CurrentIndex = 0;
		}

		public void Reset()
		{
			for (int i = 0; i < Capacity; i++)
			{
				(GameObjects[i] as IGameObject).Status = GameObjectStatus.OUTDATED;
			}
		}

		public T Get()
		{
			int read = 0;
			while((GameObjects[CurrentIndex] as IGameObject).Status != GameObjectStatus.OUTDATED && read < Capacity)
			{
				read++;
				CurrentIndex++;
				CurrentIndex %= Capacity;
			}
			if (read >= Capacity)
			{
				// Pool mal dimensionné
				Trace.WriteLine("Pool mal dimensionné : " + Capacity);
				return (new T());
			}
			else
			{
				return (GameObjects[CurrentIndex] as T);
			}
		}
	}
}