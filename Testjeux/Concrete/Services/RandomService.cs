using System;

namespace GameNameSpace
{
	public class RandomService : IRandomService
	{
		protected Random Random;

		public RandomService()
		{
			Random = new Random();
		}

		public float Next()
		{
			return ((float)Random.NextDouble());
		}

		public int Next(int min, int max)
		{
			return (Random.Next(min, max + 1));
		}
	}
}
