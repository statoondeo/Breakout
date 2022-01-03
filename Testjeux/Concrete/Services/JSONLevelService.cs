namespace GameNameSpace
{
	public class JSONLevelService : BaseLevelService
	{

		public JSONLevelService()
			: base(new JSONParser())
		{
			LevelPathes.Add(1, "Breakout.Concrete.Levels.level11.json");
			LevelPathes.Add(2, "Breakout.Concrete.Levels.level12.json");
			LevelPathes.Add(3, "Breakout.Concrete.Levels.level13.json");
			LevelPathes.Add(4, "Breakout.Concrete.Levels.level14.json");
			LevelPathes.Add(5, "Breakout.Concrete.Levels.level15.json");
			LevelPathes.Add(6, "Breakout.Concrete.Levels.level21.json");
			LevelPathes.Add(7, "Breakout.Concrete.Levels.level22.json");
			LevelPathes.Add(8, "Breakout.Concrete.Levels.level23.json");
			LevelPathes.Add(9, "Breakout.Concrete.Levels.level24.json");
			LevelPathes.Add(10, "Breakout.Concrete.Levels.level25.json");
			LevelPathes.Add(11, "Breakout.Concrete.Levels.level31.json");
			LevelPathes.Add(12, "Breakout.Concrete.Levels.level32.json");
			LevelPathes.Add(13, "Breakout.Concrete.Levels.level33.json");
			LevelPathes.Add(14, "Breakout.Concrete.Levels.level34.json");
			LevelPathes.Add(15, "Breakout.Concrete.Levels.level35.json");
		}
	}
}

