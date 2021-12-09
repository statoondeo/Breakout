using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BallExplosionParticlesEmitter : BaseParticlesEmitter
	{
		public BallExplosionParticlesEmitter(IGameObject gameObject, Texture2D texture, int number)
			: base(gameObject, texture, number)
		{ }

		public override void Emit()
		{
			Vector2 position = GameObject.Body is IBoxBody ? (GameObject.Body as IBoxBody).Position + (GameObject.Body as IBoxBody).Size * 0.5f : GameObject.Body is ICircleBody ? (GameObject.Body as ICircleBody).Center : Vector2.Zero;
			IRandomService rand = ServiceLocator.Instance.Get<IRandomService>();
			for (int i = 0; i < Number; i++)
			{
				float ttl = rand.Next() * 0.4f + 0.1f;
				float scale = rand.Next() * 1.0f + 0.2f;
				float particleSpeed = rand.Next() * 250.0f;
				float particleAngle = (float)((rand.Next() + 1) * Math.PI );
				ParticleGameObject particle = ServiceLocator.Instance.Get<IParticlesService>().GetParticle();
				particle.Init(Texture, ServiceLocator.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), position - Size * scale * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, 0.0f, 1.0f, Vector2.Zero);
				ServiceLocator.Instance.Get<ISceneService>().RegisterGameObject(particle);
			}
		}
	}
}