namespace GameNameSpace
{
	public interface ICommand
	{
		bool CanExecute { get; }
		void Execute();
	}
}
