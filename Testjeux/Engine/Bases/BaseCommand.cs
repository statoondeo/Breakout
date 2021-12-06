namespace GameNameSpace
{
	public abstract class BaseCommand : ICommand
	{
		public bool CanExecute { get; protected set; }
		protected BaseCommand()
		{
			CanExecute = true;
		}
		public abstract void Execute();
	}
}
