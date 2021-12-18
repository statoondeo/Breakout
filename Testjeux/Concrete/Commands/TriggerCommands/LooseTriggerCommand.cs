namespace GameNameSpace
{
	public class LooseTriggerCommand : BaseCommand
	{
		public LooseTriggerCommand() : base() { }

		public override void Execute()
		{
			Services.Instance.Get<ISceneService>().Loose();
		}
	}
}
