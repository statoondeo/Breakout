using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BrickExplosionParticlesEmitter : BaseParticlesEmitter
	{
		public BrickExplosionParticlesEmitter(IGameObject gameObject, Texture2D texture, int number)
			: base(gameObject, texture, number)
		{ }

		public override void Emit(CollisionTestResult collisionResult)
		{
			base.Emit(collisionResult);
			Vector2 normal = collisionResult.BodyA == GameObject ? collisionResult.Normal : Vector2.Negate(collisionResult.Normal);
			Vector2 position = GameObject.Body is IBoxBody ? GameObject.Body.Position + Size / 2 : GameObject.Body is ICircleBody ? (GameObject.Body as ICircleBody).Center : Vector2.Zero;
			IRandomService rand = ServiceLocator.Instance.Get<IRandomService>();
			for (int i = 0; i < Number; i++)
			{
				float angleSpeed = (float)(rand.Next() * Math.PI - Math.PI / 2);
				float ttl = rand.Next() * 0.3f + 0.05f;
				float scale = rand.Next() * 0.7f + 0.2f;
				float particleSpeed = rand.Next() * 100.0f * collisionResult.Depth;
				float particleAngle = (float)((rand.Next() - 0.5f) * Math.PI * 0.5f + Math.Atan2(normal.Y, normal.X));
				ParticleGameObject particle = ServiceLocator.Instance.Get<IParticlesService>().GetParticle();
				particle.Init(Texture, ServiceLocator.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), position - Size * scale * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, angleSpeed, 1.0f, Vector2.Zero);
				ServiceLocator.Instance.Get<ISceneService>().RegisterGameObject(particle);
			}
		}
	}
}