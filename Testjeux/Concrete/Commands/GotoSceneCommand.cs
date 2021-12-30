namespace GameNameSpace
{
	public class GotoSceneCommand : BaseCommand
	{
		protected SceneType TargetScene;
		protected int LevelNumber;
		protected ParsedLevel Level;

		public GotoSceneCommand(SceneType targetScene, int levelNumber)
			: base()
		{
			TargetScene = targetScene;
			LevelNumber = levelNumber;
			Level = null;
		}

		public GotoSceneCommand(SceneType targetScene, ParsedLevel level)
			: base()
		{
			TargetScene = targetScene;
			Level = level;
		}

		public override void Execute()
		{
			if (Level == null)
			{
				Services.Instance.Get<ISceneService>().ChangeScene(TargetScene, LevelNumber, new DummyCommand());
			}
			else
			{
				Services.Instance.Get<ISceneService>().ChangeScene(TargetScene, Level, new DummyCommand());
			}
		}
	}
}
