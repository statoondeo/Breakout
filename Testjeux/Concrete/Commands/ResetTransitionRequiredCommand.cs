namespace GameNameSpace
{
	public class ResetTransitionRequiredCommand : BaseCommand
	{

		public ResetTransitionRequiredCommand() : base() { }

		public override void Execute()
		{
			Services.Instance.Get<ISceneService>().ResetTransition();
		}
	}
}
