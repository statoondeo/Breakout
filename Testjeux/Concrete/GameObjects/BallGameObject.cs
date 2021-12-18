using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BallGameObject : BaseGameObject
	{
		protected static readonly float Scale = 2.0f;
		protected static readonly float BodySizeFactor = 0.5f;

		protected IParticlesEmitter TrailParticlesEmitter;
		protected IParticlesEmitter BallExplosionParticlesEmitter;
		protected IRenderable HaloRenderable;

		public bool Exploded { get; set; }

		public float Speed { get; protected set; }

		public BallGameObject(Vector2 position, float speed, Vector2 size)
			: base()
		{
			Vector2 offset = (size * BodySizeFactor - size);
			Speed = speed;
			Type = GameObjectType.BALL;
			Body = new BallBody(position, size * Scale * BodySizeFactor, Vector2.Zero, new BallColliderCommand(this, new BallImpactParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow))));
			Movable = new VelocityMovable(this);
			Renderable = new AnimatedTextureRenderable(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedBullet), offset, new Point(32), Scale, new TextureAnimation(new int[] { 0, 1 ,2 ,3 ,4 ,5 ,6 ,7 ,8 ,9 ,10 ,11 ,12 ,13, 14, 15 }, 0.05f, true));
			TrailParticlesEmitter = new BallTrailParticlesEmitter(this, Services.Instance.Get<IShapeService>().CropTexture(Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedBullet), new Rectangle(new Point(224, 0), new Point(32))));
			BallExplosionParticlesEmitter = new BallExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 25);
			Exploded = false;
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
			HaloRenderable = new TextureRenderable(this, texture, 1.0f, 0.5f * (new Vector2(64) - (new Vector2(texture.Width, texture.Height))));
			HaloRenderable.Alpha = 0.1f;
			HaloRenderable.ColorMask = Color.OrangeRed;
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

		public override void Draw(SpriteBatch spriteBatch)
		{
			HaloRenderable.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}
	}
}