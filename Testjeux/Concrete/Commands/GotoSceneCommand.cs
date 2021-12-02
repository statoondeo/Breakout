using System.Diagnostics;

namespace GameNameSpace
{
	public class GotoSceneCommand : BaseCommand
	{
		protected GameState.SceneType TargetScene;
		public GotoSceneCommand(GameState.SceneType targetScene)
			: base()
		{
			TargetScene = targetScene;
		}

		public override void Execute()
		{
			Trace.WriteLine("GotoSceneCommand");
			ServiceLocator.Instance.Get<GameState>().ChangeScene(TargetScene);
		}
	}
}
