namespace GameNameSpace
{
	public class WinTriggerCommand : BaseCommand
	{
		public WinTriggerCommand() : base() { }

		public override void Execute()
		{
			ServiceLocator.Instance.Get<ISceneService>().Win();
		}
	}
}
