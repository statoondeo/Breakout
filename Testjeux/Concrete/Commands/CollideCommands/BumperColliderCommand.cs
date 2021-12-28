using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BumperColliderCommand : BaseColliderCommand
	{
		protected IParticlesEmitter ParticlesEmitter;

		public BumperColliderCommand(IGameObject gameObject, IParticlesEmitter particlesEmitter)
			: base(gameObject)
		{
			ParticlesEmitter = particlesEmitter;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			ParticlesEmitter.Emit(collisionResult);
			if (gameObject.Type == GameObjectType.BALL)
			{
				IBallGameObject ball = gameObject as IBallGameObject;
				Vector2 velocity = Vector2.Normalize(ball.Body.Velocity);
				ball.Body.Velocity = velocity * ball.Speed;
			}
		}
	}
}