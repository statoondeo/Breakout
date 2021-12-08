using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrickColliderCommand : BaseColliderCommand
	{
		protected BrickExplosionParticlesEmitter ParticlesEmitterGameObject;

		public BrickColliderCommand(IGameObject gameObject, BrickExplosionParticlesEmitter particlesEmitterGameObject) 
			: base(gameObject)
		{
			ParticlesEmitterGameObject = particlesEmitterGameObject;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			if (gameObject.Type == GameObjectType.BALL)
			{
				GameObject.Status = GameObjectStatus.OUTDATED;
				ParticlesEmitterGameObject.Emit();
			}
		}
	}
}