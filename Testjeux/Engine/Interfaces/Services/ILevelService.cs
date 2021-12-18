namespace GameNameSpace
{
	public interface ILevelService : IService
	{
		int MaxLevel { get; }
		string GetPath(int levelNumber);
		ParsedLevel GetLevel(int levelNumber);
	}
}

