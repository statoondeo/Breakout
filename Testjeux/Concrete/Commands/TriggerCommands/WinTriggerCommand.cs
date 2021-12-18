namespace GameNameSpace
{
	public class WinTriggerCommand : BaseCommand
	{
		public WinTriggerCommand() : base() { }

		public override void Execute()
		{
			Services.Instance.Get<ISceneService>().Win();
		}
	}
}
