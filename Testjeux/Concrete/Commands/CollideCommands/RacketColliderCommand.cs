namespace GameNameSpace
{
	public class RacketColliderCommand : BaseColliderCommand
	{
		protected IParticlesEmitter ParticlesEmitter;

		public RacketColliderCommand(IGameObject gameObject, IParticlesEmitter particlesEmitter)
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