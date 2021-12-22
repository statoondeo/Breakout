using System.Collections.Generic;
using System.Diagnostics;

namespace GameNameSpace
{
	public class ObjectPool<T> where T : class, new()
	{
		protected IList<T> GameObjects;
		protected int CurrentIndex;
		public int Capacity { get; protected set; }
		
		public ObjectPool(int capacity)
		{
			Capacity = capacity;
			GameObjects = new List<T>();
			for (int i = 0; i < Capacity; i++)
			{
				T newObject = new T();
				GameObjects.Add(newObject);
				(newObject as IGameObject).Status = GameObjectStatus.OUTDATED;
			}
			CurrentIndex = 0;
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
				T newObject = new T();
				Capacity++;
				GameObjects.Add(newObject);
				return (newObject);
			}
			else
			{
				return (GameObjects[CurrentIndex] as T);
			}
		}
	}
}