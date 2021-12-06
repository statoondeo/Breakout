namespace GameNameSpace
{
	public class DummyColliderCommand : BaseColliderCommand
	{
		public DummyColliderCommand() : base(null) { }

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
		}
	}
}