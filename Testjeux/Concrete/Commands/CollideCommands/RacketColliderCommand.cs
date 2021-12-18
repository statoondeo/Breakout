using Microsoft.Xna.Framework;

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
			if (gameObject.Type == GameObjectType.BALL)
			{
				BallGameObject ball = gameObject as BallGameObject;
				Vector2 velocity = Vector2.Normalize(ball.Body.Velocity);
				float speed = Vector2.Dot(velocity, ball.Body.Velocity);
				if (speed < ball.Speed)
				{
					speed *= 1.5f;
					speed = MathHelper.Clamp(speed, 0, ball.Speed);
					ball.Body.Velocity = velocity * speed;
				}
			}
			Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new TextureRenderable(null, Services.Instance.Get<IAssetService>().GetTexture(TextureName.Platform), 1.0f, Vector2.Zero), GameObject.Body.Position, (GameObject as RacketGameObject).Size, 0.3f, 1.2f));
		}
	}
}