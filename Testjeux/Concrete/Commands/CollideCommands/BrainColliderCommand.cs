using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BrainColliderCommand : BaseColliderCommand
	{
		protected IParticlesEmitter ParticlesEmitter;

		public BrainColliderCommand(IGameObject gameObject, IParticlesEmitter particlesEmitter)
			: base(gameObject)
		{
			ParticlesEmitter = particlesEmitter;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			(GameObject as IBrickGameObject).Damage();
			if ((GameObject as IBrickGameObject).Health <= 0)
			{
				GameObject.Status = GameObjectStatus.OUTDATED;
				IRandomService rand = Services.Instance.Get<IRandomService>();
				Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
				Vector2 position = GameObject.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * GameObject.Renderable.Scale * 0.25f;
				for (int i = 0; i < 5; i++)
				{
					float angleSpeed = 0;
					float ttl = rand.Next() * 1.4f + 0.1f;
					float scale = rand.Next() * 1.4f + 0.1f;
					float particleSpeed = rand.Next() * 250.0f / scale;
					float particleAngle = rand.Next() * 2.0f * (float)Math.PI;

					Services.Instance.Get<IParticlesService>().Register(texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), position - texture.Bounds.Size.ToVector2() * scale * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, angleSpeed, 1.0f, 0.0f, Vector2.Zero);
				}
			}
			else
			{
				Vector2 position = GameObject.Body.Position - BrainGameObject.TextureSize * BrainGameObject.BodySizeFactor * GameObject.Renderable.Scale * 0.25f;
				for (int i = 0; i < 3; i++)
{
					Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new BrainIdleRenderable(null, 1.0f, Vector2.Zero), position, BrainGameObject.TextureSize, i * 0.2f, i * 0.2f + 0.6f));
				}
				ParticlesEmitter.Emit(collisionResult);
				if (gameObject.Type == GameObjectType.BALL)
				{
					IBallGameObject ball = gameObject as IBallGameObject;
					Vector2 velocity = Vector2.Normalize(ball.Body.Velocity);
					ball.Body.Velocity = velocity * ball.Speed;
}

				IStateContainer container = GameObject as IStateContainer;
				if (((GameObject as IBrickGameObject).Health % 2 == 0) && (container.CurrentState is BrainAttackState))
				{
					container.CurrentState = container.CurrentState.Transitions[0];
				}
			}
		}
	}
}