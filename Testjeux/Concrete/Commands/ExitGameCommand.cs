namespace GameNameSpace
{
	public class ExitGameCommand : BaseCommand
	{

		public ExitGameCommand() : base() { }

		public override void Execute()
		{
			Services.Instance.Get<ISceneService>().ExitRequired = true;
		}
	}
}
