namespace GameNameSpace
{
	public class LooseTriggerCommand : BaseCommand
	{
		public LooseTriggerCommand() : base() { }

		public override void Execute()
		{
			ServiceLocator.Instance.Get<ISceneService>().Loose();
		}
	}
}
