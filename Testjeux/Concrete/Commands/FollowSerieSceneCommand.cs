namespace GameNameSpace
{
	public class FollowSerieSceneCommand : BaseCommand
	{
		protected ICommand GotoTargetScene;
		protected int LevelNumber;

		public FollowSerieSceneCommand(SceneType targetScene, int levelNumber = 0)
			: base()
		{
			GotoTargetScene = new SwitchSceneCommand(targetScene, levelNumber);
			LevelNumber = levelNumber;
		}

		public override void Execute()
		{
			if (LevelNumber == 1)
			{
				Services.Instance.Get<ISceneService>().Life = Services.Instance.Get<ISceneService>().MaxLife;
			}
			Services.Instance.Get<ISceneService>().Mode = SceneModeNames.Serie;
			GotoTargetScene.Execute();
		}
	}
}
