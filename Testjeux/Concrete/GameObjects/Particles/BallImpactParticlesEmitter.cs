using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BallImpactParticlesEmitter : BaseParticlesEmitter
	{
		public BallImpactParticlesEmitter(IGameObject gameObject, Texture2D texture)
			: base(gameObject, texture, 0)
		{ }

		public override void Emit(CollisionTestResult collisionResult)
		{
			IBody body = GameObject.Body;
			Vector2 position = body is IBoxBody ? (body as IBoxBody).Position + (body as IBoxBody).Size * 0.5f : body is ICircleBody ? (body as ICircleBody).Center : Vector2.Zero;
			IRandomService rand = ServiceLocator.Instance.Get<IRandomService>();

			float angleSpeed = 0;
			float ttl = rand.Next() * 0.2f + 0.05f;
			float scale = rand.Next() * 0.8f + 0.2f;
			float particleSpeed = 0;
			float particleAngle = 0;

			ServiceLocator.Instance.Get<IParticlesService>().Create(Texture, ServiceLocator.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), position - Size * scale * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, angleSpeed, 1.0f, Vector2.Zero);
		}
	}
}