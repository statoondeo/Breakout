using System.Collections.Generic;

namespace GameNameSpace
{
	public abstract class BaseLevelService : ILevelService
	{
		protected IDictionary<int, string> LevelPathes;
		protected IParser Factory;

		protected BaseLevelService(IParser factory)
		{
			Factory = factory;
			LevelPathes = new Dictionary<int, string>();
		}

		public virtual string GetPath(int levelNumber)
		{
			return (LevelPathes[levelNumber]);
		}

		public virtual ParsedLevel GetLevel(int levelNumber)
		{
			return (Factory.ReadEmbeddedResource(GetPath(levelNumber)));
		}
	}
}

