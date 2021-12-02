namespace GameNameSpace
{
	public class GotoSceneCommand : BaseCommand
	{
		protected SceneType TargetScene;
		public GotoSceneCommand(SceneType targetScene)
			: base()
		{
			TargetScene = targetScene;
		}

		public override void Execute()
		{
			ServiceLocator.Instance.Get<GameState>().ChangeScene(TargetScene);
		}
	}
}
