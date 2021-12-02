namespace GameNameSpace
{
	public interface ICollideCommand
	{
		bool CanExecute { get; }
		void Execute(ICollidable collidable);
	}
}
