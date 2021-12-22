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
				Vector2 textureSize = new Vector2(texture.Width, texture.Height);
				Vector2 position = GameObject.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * GameObject.Renderable.Scale * 0.25f;
				for (int i = 0; i < 5; i++)
				{
					float angleSpeed = 0;
					float ttl = rand.Next() * 1.4f + 0.1f;
					float scale = rand.Next() * 1.4f + 0.1f;
					float particleSpeed = rand.Next() * 250.0f / scale;
					float particleAngle = rand.Next() * 2.0f * (float)Math.PI;

					Services.Instance.Get<IParticlesService>().Register(texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), position - textureSize * scale * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, angleSpeed, 1.0f, 0.0f, Vector2.Zero);
				}
			}
			else
			{
				Vector2 position = GameObject.Body.Position - BrainBrickGameObject.TextureSize * BrainBrickGameObject.BodySizeFactor * GameObject.Renderable.Scale * 0.25f;
				for (int i = 0; i < 3; i++)
{
					Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new BrainAnimatedTextureRenderable(null, 1.0f, Vector2.Zero), position, BrainBrickGameObject.TextureSize, i * 0.2f, i * 0.2f + 0.6f));
				}
				ParticlesEmitter.Emit(collisionResult);
				if (gameObject.Type == GameObjectType.BALL)
				{
					BallGameObject ball = gameObject as BallGameObject;
					Vector2 velocity = Vector2.Normalize(ball.Body.Velocity);
					ball.Body.Velocity = velocity * ball.Speed;
				}

				if ((GameObject as IBrickGameObject).Health % 2 == 0)
				{
					Vector2 destination = new Vector2(522, 88);
					Vector2 origin = new Vector2(destination.X, -300);
					IGameObject ShieldGameObject = new ShieldBrickGameObject(Vector2.Zero, 2.1f);
					Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(ShieldGameObject, origin, destination));

					Services.Instance.Get<ISceneService>().RegisterGameObject(new BrainShield1Trigger());

					destination = new Vector2(168, 208);
					origin = new Vector2(destination.X, -300);
					Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(new WobblerBrickGameObject(Vector2.Zero, 1.0f), origin, destination));

					destination = new Vector2(1048, 208);
					origin = new Vector2(destination.X, -300);
					Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(new WobblerBrickGameObject(Vector2.Zero, 1.0f), origin, destination));
				}
			}
		}
	}
}