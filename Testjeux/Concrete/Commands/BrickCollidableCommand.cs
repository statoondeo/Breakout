namespace GameNameSpace
{
	public class BrickCollidableCommand : BaseCollidableCommand
	{
		protected IGameObject GameObject;
		public BrickCollidableCommand(IGameObject gameObject) 
		{
			GameObject =  gameObject;
		}

		public override void Execute(ICollidable collidable)
		{
			GameObject.Status = GameObjectStatus.OUTDATED;
		}
	}
}