using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RacketImpactParticlesEmitter : BaseParticlesEmitter
	{
		public RacketImpactParticlesEmitter(IGameObject gameObject, Texture2D texture, int number)
			: base(gameObject, texture, number)
		{ }

		public override void Emit(CollisionTestResult collisionResult)
		{
			IBoxBody body = (GameObject.Body as ICompositeIntersecBody).CollisionCheckerBody as IBoxBody;
			IRandomService rand = Services.Instance.Get<IRandomService>();
			for (int i = 0; i < Number; i++)
			{
				float angleSpeed = (float)(rand.Next() * 2 * Math.PI - Math.PI) * 0.5f;
				float ttl = rand.Next() * 0.5f + 0.1f;
				float scale = rand.Next() * 0.5f + 0.25f;
				float particleSpeed = rand.Next() * 150.0f;
				float particleAngle = (float)(rand.Next() * Math.PI * 0.5f + Math.PI * 0.25f);
				Vector2 rotationOrigin = new(rand.Next() * Size.X * scale, rand.Next() * Size.Y * scale);
				Services.Instance.Get<IParticlesService>().Register(Texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), body.Position + (body.Size - Size * scale) * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, angleSpeed, 1.0f, 0.0f, rotationOrigin);
			}
		}
	}
}