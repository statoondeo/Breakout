namespace GameNameSpace
{
	public interface IColliderCommand
	{
		bool CanExecute { get; }
		void Execute(IGameObject gameObject, CollisionTestResult collisionResult);
	}
}
