namespace GameNameSpace
{
	public class RacketCollidableCommand : BaseCollidableCommand
	{
		protected IGameObject GameObject;

		public RacketCollidableCommand(IGameObject gameObject)
		{
			GameObject = gameObject;
		}

		public override void Execute(ICollidable collidable)
		{
		}
	}
}