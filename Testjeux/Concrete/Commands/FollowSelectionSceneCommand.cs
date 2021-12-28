namespace GameNameSpace
{
	public class FollowSelectionSceneCommand : BaseCommand
	{
		protected ICommand GotoTargetScene;

		public FollowSelectionSceneCommand(SceneType targetScene, int levelNumber)
			: base()
		{
			GotoTargetScene = new SwitchSceneCommand(targetScene, levelNumber);
		}

		public override void Execute()
		{
			Services.Instance.Get<ISceneService>().Life = 3;
			Services.Instance.Get<ISceneService>().Mode = SceneModeNames.Selection;
			GotoTargetScene.Execute();
		}
	}
}
