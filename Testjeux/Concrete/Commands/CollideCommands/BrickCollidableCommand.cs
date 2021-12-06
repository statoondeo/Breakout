namespace GameNameSpace
{
	public class BrickColliderCommand : BaseColliderCommand
	{
		public BrickColliderCommand(IGameObject gameObject) 
			: base(gameObject)
		{ }

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			GameObject.Status = gameObject.Type == GameObjectType.BALL ? GameObjectStatus.OUTDATED : GameObject.Status;
		}
	}
}