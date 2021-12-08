using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BallTrailParticlesEmitter : BaseParticlesEmitter
	{
		protected float CurrentTtl;
		protected float MaxTtl;

		public BallTrailParticlesEmitter(IGameObject gameObject, Texture2D texture, int number)
			: base(gameObject, texture, number)
		{
			CurrentTtl = MaxTtl = 0.03f;
		}

		public override void Emit(GameTime gameTime)
		{
			CurrentTtl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (CurrentTtl < 0)
			{
				CurrentTtl = MaxTtl;
				Vector2 position = GameObject.Body is ICircleBody ? (GameObject.Body as ICircleBody).Center : Vector2.Zero;
				for (int i = 0; i < Number; i++)
				{
					float ttl = 0.25f;
					float scale = 0.5f;
					float particleSpeed = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * 30 * gameTime.ElapsedGameTime.TotalSeconds) ;
					Vector2 particleVelocity = Vector2.Negate(GameObject.Body.Velocity);
					particleVelocity *= new Vector2((float)Math.Cos(ServiceLocator.Instance.Get<Random>().NextDouble()), (float)Math.Sin(ServiceLocator.Instance.Get<Random>().NextDouble()));
					ParticleGameObject particle = ServiceLocator.Instance.Get<ParticleService>().GetParticle();
					particle.Init(Texture, position - Size * scale * 0.5f, particleVelocity * particleSpeed, scale, ttl, 0, 0.5f, Vector2.Zero);
					ServiceLocator.Instance.Get<GameState>().CurrentScene.GeneratedGameObjectsCollection.Add(particle);
				}
			}
		}
	}
}