namespace GameNameSpace
{
	public abstract class BaseColliderCommand : IColliderCommand
	{
		protected IGameObject GameObject;
		public bool CanExecute { get; protected set; }

		protected BaseColliderCommand(IGameObject gameobject)
		{
			GameObject = gameobject;
			CanExecute = true;
		}

		public virtual void Execute(IGameObject gameObject, CollisionTestResult collisionResult) 
		{
		}
	}
}
