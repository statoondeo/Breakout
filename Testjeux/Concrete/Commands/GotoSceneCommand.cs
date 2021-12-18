﻿namespace GameNameSpace
{
	public class GotoSceneCommand : BaseCommand
	{
		protected SceneType TargetScene;
		protected int LevelNumber;

		public GotoSceneCommand(SceneType targetScene, int levelNumber)
			: base()
		{
			TargetScene = targetScene;
			LevelNumber = levelNumber;
		}

		public override void Execute()
		{
			Services.Instance.Get<ISceneService>().ChangeScene(TargetScene, LevelNumber, new DummyCommand());
		}
	}
}
