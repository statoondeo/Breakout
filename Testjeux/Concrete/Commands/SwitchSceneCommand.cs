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

		public SwitchSceneCommand(SceneType targetScene, ParsedLevel level)
			: base()
		{
			GotoTargetScene = new GotoSceneCommand(targetScene, level);
		}

		public override void Execute()
		{
			Services.Instance.Get<ISceneService>().UnLoad(GotoTargetScene);
		}
	}
}
