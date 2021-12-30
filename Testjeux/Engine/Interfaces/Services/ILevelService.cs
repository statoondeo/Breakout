namespace GameNameSpace
{
	public interface ILevelService : IService
	{
		int MaxLevel { get; }
		string GetPath(int levelNumber);
		ParsedLevel GetLevel(int levelNumber);
		ParsedLevel Load(string resourceName);
		void Save(ParsedLevel level, string resourceName);
	}
}

