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
			(GameObject as BrickGameObject).Damage();
			if ((GameObject as BrickGameObject).Health <= 0)
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
			}
		}
	}
	public class BrainParticlesEmitter : BaseParticlesEmitter
	{
		public BrainParticlesEmitter(IGameObject gameObject) 
			: base(gameObject, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 15)
		{
		}

		public override void Emit(CollisionTestResult collisionResult)
		{
			base.Emit(collisionResult);
			IRandomService rand = Services.Instance.Get<IRandomService>();
			Vector2 textureSize = new Vector2(Texture.Width, Texture.Height);
			float angleSpeed = 0;
			float ttl;
			float scale;
			float particleSpeed;
			float particleAngle;

			for (int i = 0; i < 5; i++)
			{
				ttl = rand.Next() * 0.8f + 0.2f;
				scale = rand.Next() * 1.2f + 0.3f;
				particleSpeed = rand.Next() * 200.0f / scale;
				particleAngle = rand.Next() * 2.0f * (float)Math.PI;

				Services.Instance.Get<IParticlesService>().Register(Texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), GameObject.Body.Position - textureSize * scale * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, angleSpeed, 1.0f, 0.0f, Vector2.Zero);
			}
		}
	}
	public class ShieldColliderCommand : BaseColliderCommand
	{
		protected ShieldBrickGameObject ShieldGameObject;
		public ShieldColliderCommand(IGameObject gameObject)
			: base(gameObject)
		{
			ShieldGameObject = gameObject as ShieldBrickGameObject;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);

			// Vibration lors du rebond
			Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new ShieldTextureRenderable(null, 1.0f, Vector2.Zero), GameObject.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * GameObject.Renderable.Scale * 0.25f, ShieldBrickGameObject.TextureSize, 0.25f, 1.9f));
			Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new ShieldTextureRenderable(null, 1.0f, Vector2.Zero), GameObject.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * GameObject.Renderable.Scale * 0.25f, ShieldBrickGameObject.TextureSize, 0.5f, 2.1f));
			Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new ShieldTextureRenderable(null, 1.0f, Vector2.Zero), GameObject.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * GameObject.Renderable.Scale * 0.25f, ShieldBrickGameObject.TextureSize, 0.75f, 2.3f));

			if (gameObject.Type == GameObjectType.BALL)
			{
				BallGameObject ball = gameObject as BallGameObject;
				Vector2 velocity = Vector2.Normalize(ball.Body.Velocity);
				ball.Body.Velocity = velocity * ball.Speed;
			}
		}
	}// new ShieldTextureRenderable(this, 1.0f, Vector2.Zero);
	public class ElasticZoomGameObject : BaseGameObject
	{
		protected float Ttl;
		protected float CurrentTtl;
		protected float TargetScale;
		protected ITweening Tweening;
		protected Vector2 Size;

		public ElasticZoomGameObject(IRenderable renderable, Vector2 position, Vector2 size, float ttl, float targetScale)
			: base()
{
			Size = size;
			TargetScale = targetScale;
			Body = new InvisibleBody(position);
			Ttl = ttl;
			CurrentTtl = 0;
			Tweening = Services.Instance.Get<ITweeningService>().Get(TweeningName.ElasticOut);
			Renderable = renderable;
			(Renderable as TextureRenderable).GameObject = this;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			CurrentTtl += (float)gameTime.ElapsedGameTime.TotalSeconds;
			Renderable.Scale = Tweening.GetStep(CurrentTtl / Ttl) * TargetScale;
			Renderable.Offset = Size * (1 - Renderable.Scale) * 0.5f;
			if (CurrentTtl > Ttl)
			{
				Status = GameObjectStatus.OUTDATED;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch, (Ttl - CurrentTtl) / Ttl, 0.0f, Renderable.Scale, Vector2.Zero);
		}
	}
}