namespace GameNameSpace
{
	public class JSONLevelService : BaseLevelService
	{

		public JSONLevelService()
			: base(new JSONParser())
		{
			LevelPathes.Add(1, "Breakout.Concrete.Levels.level1.json");
		}
	}
}

