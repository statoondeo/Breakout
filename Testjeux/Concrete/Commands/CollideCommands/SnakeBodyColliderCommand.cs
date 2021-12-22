﻿using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class SnakeBodyColliderCommand : BaseColliderCommand
	{
		protected IParticlesEmitter ParticlesEmitterGameObject;

		public SnakeBodyColliderCommand(IGameObject gameObject, IParticlesEmitter particlesEmitterGameObject)
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
				BallGameObject ball = gameObject as BallGameObject;
				Vector2 velocity = Vector2.Normalize(ball.Body.Velocity);
				float speed = Vector2.Dot(velocity, ball.Body.Velocity);
				if (speed < ball.Speed)
				{
					speed *= 1.1f;
					speed = MathHelper.Clamp(speed, 0, ball.Speed);
					ball.Body.Velocity = velocity * speed;
				}
			}
		}
	}
}