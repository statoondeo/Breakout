using System.Diagnostics;

namespace GameNameSpace
{
	public class SwitchSceneCommand : BaseCommand
	{
		protected ICommand GotoTargetScene;
		public SwitchSceneCommand(ICommand gotoTargetScene)
			: base()
		{
			GotoTargetScene = gotoTargetScene;
		}

		public override void Execute()
		{
			Trace.WriteLine("SwitchSceneCommand");
			ServiceLocator.Instance.Get<GameState>().CurrentScene.UnLoad(GotoTargetScene);
		}
	}
}
