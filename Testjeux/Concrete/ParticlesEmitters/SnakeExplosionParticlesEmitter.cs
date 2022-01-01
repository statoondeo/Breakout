using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class SnakeExplosionParticlesEmitter : BaseParticlesEmitter
	{
		public SnakeExplosionParticlesEmitter(IGameObject gameObject, Texture2D texture, int number)
			: base(gameObject, texture, number)
		{ }

		public override void Emit()
		{
			base.Emit();
			IRandomService rand = Services.Instance.Get<IRandomService>();
			for (int i = 0; i < Number; i++)
			{
				float angleSpeed = (float)(rand.Next() * Math.PI - Math.PI / 2);
				float ttl = rand.Next() * 1.3f + 0.2f;
				float scale = rand.Next() * 1.6f + 0.2f;
				float particleSpeed = rand.Next() * 90 * 1 / scale;
				float particleAngle = 2.0f * (float)Math.PI * rand.Next();
				Vector2 particleVelocity = new((float)Math.Cos(particleAngle), (float)Math.Sin(particleAngle));
				particleVelocity *= particleSpeed;
				Services.Instance.Get<IParticlesService>().Register(Texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), GameObject.Body.Position - Size * scale * 0.5f, particleVelocity, scale, ttl, angleSpeed, 1.0f, 0.0f, Vector2.Zero);
			}
		}
	}
}