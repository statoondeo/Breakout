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
			IRandomService rand = Services.Instance.Get<IRandomService>();
			for (int i = 0; i < Number; i++)
			{
				float angleSpeed = (float)(rand.Next() * Math.PI - Math.PI / 2);
				float ttl = rand.Next() * 0.8f + 0.2f;
				float scale = rand.Next() * 0.6f + 0.2f;
				float particleSpeed = rand.Next() * 15 * collisionResult.Depth * 1 / scale;
				float particleAngle = (float)((rand.Next() - 0.5f) * Math.PI * 0.5f + Math.Atan2(normal.Y, normal.X));
				Vector2 particleVelocity = new Vector2((float)Math.Cos(particleAngle), (float)Math.Sin(particleAngle));
				particleVelocity *= particleSpeed;
				Services.Instance.Get<IParticlesService>().Register(Texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), position - Size * scale * 0.5f, particleVelocity, scale, ttl, angleSpeed, 1.0f, 0.0f, Vector2.Zero);
			}
		}
	}
	public class SpawnBlobCommand : BaseCommand
	{
		protected IGameObject GameObject;
		protected Vector2 Destination;
		protected float Ttl;

		public SpawnBlobCommand(IGameObject gameObject, Vector2 destination, float ttl)
			: base()
		{
			GameObject = gameObject;
			Destination = destination;
			Ttl = ttl;
		}

		public override void Execute()
		{
			Services.Instance.Get<ISceneService>().RegisterGameObject(new BlobBrickGameObject(GameObject.Body.Position, 1.0f, Destination, Ttl));
		}
	}
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