namespace GameNameSpace
{
	public class SwitchSceneCommand : BaseCommand
	{
		protected ICommand GotoTargetScene;

		public SwitchSceneCommand(SceneType targetScene)
			: base()
		{
			GotoTargetScene = new GotoSceneCommand(targetScene);
		}

		public override void Execute()
		{
			ServiceLocator.Instance.Get<ISceneService>().UnLoad(GotoTargetScene);
		}
	}
}
