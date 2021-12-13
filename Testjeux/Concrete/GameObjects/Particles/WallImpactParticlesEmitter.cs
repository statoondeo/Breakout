using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class WallImpactParticlesEmitter : BaseParticlesEmitter
	{
		public WallImpactParticlesEmitter(IGameObject gameObject, Texture2D texture, int number)
			: base(gameObject, texture, number)
		{ }

		public override void Emit(CollisionTestResult collisionResult)
		{
			IBody body = collisionResult.BodyA == GameObject ? collisionResult.BodyB : collisionResult.BodyA;
			Vector2 position = body is IBoxBody ? (body as IBoxBody).Position + (body as IBoxBody).Size * 0.5f : body is ICircleBody ? (body as ICircleBody).Center : Vector2.Zero;
			IRandomService rand = ServiceLocator.Instance.Get<IRandomService>();
			for (int i = 0; i < Number; i++)
			{
				float angleSpeed = (float)(rand.Next() * Math.PI - Math.PI / 2);
				float ttl = (float)(rand.Next() * 0.5 + 0.1);
				float scale = (float)(rand.Next() * 0.7 + 0.2);
				float particleSpeed = rand.Next() * 250.0f;
				float particleAngle = (float)(rand.Next() * 2 * Math.PI);
				ServiceLocator.Instance.Get<IParticlesService>().Create(Texture, ServiceLocator.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), position - Size * scale * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, angleSpeed, 1.0f, Vector2.Zero);
			}
		}
	}
}