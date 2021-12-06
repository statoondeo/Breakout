namespace GameNameSpace
{
	public class OneShotBallColliderCommand : BaseColliderCommand
	{
		public OneShotBallColliderCommand(IGameObject gameObject) 
			: base(gameObject)
		{ }

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			if (gameObject.Type == GameObjectType.BRICK)
			{
				GameObject.Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}