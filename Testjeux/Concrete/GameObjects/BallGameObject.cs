using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BallGameObject : BaseGameObject
	{
		protected static readonly float Scale = 2.0f;
		protected static readonly float BodySizeFactor = 0.5f;

		protected IParticlesEmitter TrailParticlesEmitter;
		protected IParticlesEmitter BallExplosionParticlesEmitter;
		public bool Exploded { get; set; }

		public float Speed { get; protected set; }

		public BallGameObject(Vector2 position, float speed, Vector2 size)
			: base()
		{
			Vector2 offset = (size * BodySizeFactor - size);
			Speed = speed;
			Type = GameObjectType.BALL;
			Body = new BallBody(position, size * Scale * BodySizeFactor, Vector2.Zero, new BallColliderCommand(this, new BallImpactParticlesEmitter(this, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow))));
			Movable = new VelocityMovable(this);
			Renderable = new AnimatedTextureRenderable(this, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedBullet), offset, new Point(32), Scale, new TextureAnimation(new int[] { 0, 1 ,2 ,3 ,4 ,5 ,6 ,7 ,8 ,9 ,10 ,11 ,12 ,13, 14, 15 }, 0.05f, true));
			TrailParticlesEmitter = new BallTrailParticlesEmitter(this, ServiceLocator.Instance.Get<IShapeService>().CropTexture(ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedBullet), new Rectangle(new Point(224, 0), new Point(32))));
			BallExplosionParticlesEmitter = new BallExplosionParticlesEmitter(this, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 25);
			Exploded = false;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			TrailParticlesEmitter.Emit(gameTime);
			if (Exploded)
			{
				BallExplosionParticlesEmitter.Emit();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}