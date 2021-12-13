namespace GameNameSpace
{
	public class SwitchSceneCommand : BaseCommand
	{
		protected ICommand GotoTargetScene;

		public SwitchSceneCommand(SceneType targetScene, int levelNumber = 0)
			: base()
		{
			GotoTargetScene = new GotoSceneCommand(targetScene, levelNumber);
		}

		public override void Execute()
		{
			ServiceLocator.Instance.Get<ISceneService>().UnLoad(GotoTargetScene);
		}
	}
}
