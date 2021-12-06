namespace GameNameSpace
{
	public class WallCollidableCommand : BaseCollidableCommand
	{
		protected IGameObject GameObject;
		public WallCollidableCommand(IGameObject gameObject) 
		{
			GameObject =  gameObject;
		}

		public override void Execute(ICollidable collidable)
		{
		}
	}
}