namespace GameNameSpace
{
	public class BrickColliderCommand : BaseColliderCommand
	{
		protected IParticlesEmitter ParticlesEmitterGameObject;

		public BrickColliderCommand(IGameObject gameObject, IParticlesEmitter particlesEmitterGameObject) 
			: base(gameObject)
		{
			ParticlesEmitterGameObject = particlesEmitterGameObject;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			if ((gameObject.Type == GameObjectType.BALL) || (gameObject.Type == GameObjectType.RACKET))
			{
				IBrickGameObject brick = GameObject as IBrickGameObject;
				brick.Damage();
				if (brick.Health <= 0)
				{
					GameObject.Status = GameObjectStatus.OUTDATED;
					ParticlesEmitterGameObject.Emit(collisionResult);
				}
			}
		}
	}
}