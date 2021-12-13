namespace GameNameSpace
{
	public class XMLLevelService : BaseLevelService
	{

		public XMLLevelService()
			: base(new XMLParser())
		{
			LevelPathes.Add(1, "Breakout.Concrete.Levels.level1.xml");
		}
	}
}

