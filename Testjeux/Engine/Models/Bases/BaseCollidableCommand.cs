namespace GameNameSpace
{
	public abstract class BaseCollidableCommand : ICollideCommand
	{
		public bool CanExecute { get; protected set; }
		protected BaseCollidableCommand()
		{
			CanExecute = true;
		}
		public abstract void Execute(ICollidable collidable);
	}
}
