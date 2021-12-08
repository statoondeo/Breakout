using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RacketTrailParticlesEmitter : BaseParticlesEmitter
	{
		protected float CurrentTtl;
		protected float MaxTtl;

		public RacketTrailParticlesEmitter(IGameObject gameObject, Texture2D texture, int number)
			: base(gameObject, texture, number)
		{
			CurrentTtl = MaxTtl = 0.2f;
		}

		public override void Emit(GameTime gameTime)
		{
			CurrentTtl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (CurrentTtl < 0)
			{
				CurrentTtl = MaxTtl;
				IBoxBody body = (GameObject.Body as ICompositeIntersecBody).CollisionCheckerBody as IBoxBody;
				for (int i = 0; i < Number; i++)
				{
					float angleSpeed = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * 2 * Math.PI - Math.PI) * 0.5f;
					float ttl = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * 0.5 + 0.1);
					float scale = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * 0.5 + 0.25);
					float particleSpeed = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * 150);
					float particleAngle = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * Math.PI * 0.5f + Math.PI * 0.25f);
					Vector2 rotationOrigin = new Vector2((float)ServiceLocator.Instance.Get<Random>().NextDouble() * Size.X * scale, (float)ServiceLocator.Instance.Get<Random>().NextDouble() * Size.Y * scale);
					ParticleGameObject particle = ServiceLocator.Instance.Get<ParticleService>().GetParticle();
					particle.Init(Texture, body.Position + (body.Size - Size * scale) * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, angleSpeed, 1.0f, rotationOrigin);
					ServiceLocator.Instance.Get<GameState>().CurrentScene.GeneratedGameObjectsCollection.Add(particle);

				}
			}
		}
	}
}