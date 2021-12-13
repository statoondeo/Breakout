namespace GameNameSpace
{
	public interface ILevelService : IService
	{
		string GetPath(int levelNumber);
		ParsedLevel GetLevel(int levelNumber);
	}
}

