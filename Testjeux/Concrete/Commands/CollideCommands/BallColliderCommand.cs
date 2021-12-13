namespace GameNameSpace
{
	public class BallColliderCommand : BaseColliderCommand
	{
		protected IParticlesEmitter ParticlesEmitterGameObject;

		public BallColliderCommand(IGameObject gameObject, IParticlesEmitter particlesEmitterGameObject)
			: base(gameObject)
		{
			ParticlesEmitterGameObject = particlesEmitterGameObject;
		}

	public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
	{
		base.Execute(gameObject, collisionResult);
		ParticlesEmitterGameObject.Emit(collisionResult);
	}
}
}