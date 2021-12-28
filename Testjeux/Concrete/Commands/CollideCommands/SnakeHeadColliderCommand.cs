using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class SnakeHeadColliderCommand : BaseColliderCommand
	{
		protected IParticlesEmitter ParticlesEmitterGameObject;

		public SnakeHeadColliderCommand(IGameObject gameObject, IParticlesEmitter particlesEmitterGameObject)
			: base(gameObject)
		{
			ParticlesEmitterGameObject = particlesEmitterGameObject;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			if (gameObject.Type == GameObjectType.BALL)
			{
				IBrickGameObject brick = GameObject as IBrickGameObject;
				brick.Damage();
				IBallGameObject ball = gameObject as IBallGameObject;
				Vector2 velocity = Vector2.Normalize(ball.Body.Velocity);
				float speed = Vector2.Dot(velocity, ball.Body.Velocity);
				if (speed < ball.Speed)
				{
					speed *= 1.25f;
					speed = MathHelper.Clamp(speed, 0, ball.Speed);
					ball.Body.Velocity = velocity * speed;
				}
			}
		}
	}
}