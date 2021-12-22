using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BlobExplosionParticlesEmitter : BaseParticlesEmitter
	{
		public BlobExplosionParticlesEmitter(IGameObject gameObject)
			: base(gameObject, Services.Instance.Get<IShapeService>().CropTexture(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Blob), new Rectangle(Point.Zero, new Point(64))), 0)
		{ }

		public override void Emit(CollisionTestResult collisionResult)
		{
			base.Emit(collisionResult);
			Vector2 normal = collisionResult.BodyA == GameObject ? collisionResult.Normal : Vector2.Negate(collisionResult.Normal);
			Vector2 position = GameObject.Body is IBoxBody ? GameObject.Body.Position + Size / 2 : GameObject.Body is ICircleBody ? (GameObject.Body as ICircleBody).Center : Vector2.Zero;
			IRandomService rand = Services.Instance.Get<IRandomService>();
			Number = rand.Next(5, 11);
			for (int i = 0; i < Number; i++)
			{
				float ttl = rand.Next() * 0.8f + 0.2f;
				float particleSpeed = rand.Next() * 350.0f + 350.0f;
				float particleAngle = (float)((rand.Next() - 0.5f) * Math.PI * 0.5f + Math.Atan2(normal.Y, normal.X));
				IGameObject particle = Services.Instance.Get<IParticlesService>().Create(Texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), position - Size * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), 1.0f, ttl, 0.0f, 1.0f, 0.0f, Vector2.Zero);
				Services.Instance.Get<ISceneService>().RegisterGameObject(new OnOutdateParticleDecoratorGameObject(particle, new SpawnBlobCommand(particle, particle.Body.Position, rand.Next() * 30.0f + 15.0f)));
			}
		}
	}
}