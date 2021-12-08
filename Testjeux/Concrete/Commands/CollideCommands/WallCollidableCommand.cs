namespace GameNameSpace
{
	public class WallColliderCommand : BaseColliderCommand
	{
		protected IParticlesEmitter ParticlesEmitter;

		public WallColliderCommand(IGameObject gameObject, IParticlesEmitter particlesEmitter) 
			: base(gameObject)
		{
			ParticlesEmitter = particlesEmitter;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			ParticlesEmitter.Emit(collisionResult);
		}
	}
}