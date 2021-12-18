using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class RockWallGameObject : WallGameObject
	{
		public RockWallGameObject(Vector2 position)
			: base(position, new Vector2(64))
		{
			int textureIndex = Services.Instance.Get<IRandomService>().Next(0, 5);
			Texture2D texture = Services.Instance.Get<IShapeService>().CropTexture(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Rocks), new Rectangle(new Point(textureIndex * 64, 0), new Point(64)));
			Renderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero);
		}
	}
	public sealed class ForkBlobBrickGameObject : BrickGameObject
	{
		public ForkBlobBrickGameObject(Vector2 position, float scale)
			: base(position, 32, 1)
		{
			Body = new BrickBody(position, 64 * 0.5f * scale * 1.0f, new BrickColliderCommand(this, new CompositeParticulesEmitter(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.GreenSpark), 25), new BlobExplosionParticlesEmitter(this))));
			Renderable = new BlobAnimatedTextureRenderable(this, scale);
		}
	}
	public sealed class BlobBrickGameObject : BrickGameObject
	{
		public BlobBrickGameObject(Vector2 position, float scale, Vector2 destination, float ttl)
			: base(position, 32, 1)
		{
			Body = new BrickBody(position, 64 * 0.5f * scale * 1.0f, new BrickColliderCommand(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.GreenSpark), 25)));
			Movable = new TweeningMovable(this);
			(Movable as TweeningMovable).Init(Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintIn), ttl, position, destination);
			Renderable = new BlobAnimatedTextureRenderable(this, scale);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}
	}
	public sealed class WobblerBrickGameObject : BrickGameObject
	{
		private IRenderable SparkRenderable;
		private IRenderable HaloRenderable;

		public WobblerBrickGameObject(Vector2 position, float scale)
			: base(position, 32, 1)
		{
			Renderable = new WobblerAnimatedTextureRenderable(this, scale);
			SparkRenderable = new AnimatedTextureRenderable(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.BlueSpark), new Vector2(-43), new Point(100), 1.5f, new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1 }, 0.09f, true));
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
			HaloRenderable = new TextureRenderable(this, texture, 1.5f, 0.5f * (new Vector2(64) - 1.5f * (new Vector2(texture.Width, texture.Height))));
			HaloRenderable.Alpha = 0.25f;
			HaloRenderable.ColorMask = Color.CornflowerBlue;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			SparkRenderable.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			HaloRenderable.Draw(spriteBatch);
			SparkRenderable.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}
	}
	public sealed class AtomBrickGameObject : BrickGameObject
	{
		public AtomBrickGameObject(Vector2 position, float scale, Vector2 center, float radius, float currentAngle, float angleSpeed)
			: base(position, 32, 2)
		{
			Movable = new RotationMovable(this, center, radius, currentAngle, angleSpeed);
			Renderable = new AtomAnimatedTextureRenderable(this, scale);
		}
	}
	public sealed class BrainBrickGameObject : BrickGameObject
	{
		public static readonly float BodySizeFactor = 0.65f;
		public static readonly Vector2 TextureSize = new Vector2(256);

		private IRenderable HaloRenderable;
		public bool HaloActivated { get; set; }

		public BrainBrickGameObject(Vector2 position, float scale)
			: base(position, TextureSize.X * 0.5f * scale * BodySizeFactor, 6)
		{
			Vector2 offset = (TextureSize * BodySizeFactor - TextureSize) * 0.5f + new Vector2(-5, 20);
			Body = new BrickBody(position, TextureSize.X * 0.5f * scale * BodySizeFactor, new BrainColliderCommand(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 25)));
			Renderable = new BrainAnimatedTextureRenderable(this, scale, offset);
			HaloActivated = false;
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
			HaloRenderable = new TextureRenderable(this, texture, 3.0f, 0.5f * (new Vector2(64) - 3.0f * (new Vector2(texture.Width, texture.Height))));
			HaloRenderable.Alpha = 0.25f;
			HaloRenderable.ColorMask = Color.Red;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (HaloActivated)
			{
				HaloRenderable.Draw(spriteBatch);
			}
			base.Draw(spriteBatch);
		}
	}
	public sealed class ShieldBrickGameObject : BumperGameObject
	{
		public static readonly float BodySizeFactor = 0.9f;
		public static readonly Vector2 TextureSize = new Vector2(122);

		public ShieldBrickGameObject(Vector2 position, float scale)
			: base(position, TextureSize.X * 0.5f * scale * BodySizeFactor)
		{
			Vector2 offset = (TextureSize * BodySizeFactor - TextureSize);
			Body = new BumperBody(position, TextureSize.X * 0.5f * scale * BodySizeFactor, new ShieldColliderCommand(this));
			Renderable = new ShieldTextureRenderable(this, scale, offset);
		}
	}
	public abstract class BrickGameObject : BaseGameObject
	{
		public int Health { get; protected set; }
		protected bool IsDamaged;
		protected float DamageTtl;
		protected float MaxDamageTtl;

		protected BrickGameObject(Vector2 position, float radius, int health)
			: base()
		{
			Type = GameObjectType.BRICK;
			Body = new BrickBody(position, radius, new BrickColliderCommand(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 15)));
			Health = health;
			MaxDamageTtl = 0.1f;
		}

		public void Damage()
		{
			Health--;
			IsDamaged = true;
			DamageTtl = MaxDamageTtl;
			Renderable.ColorMask = Color.Red;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (IsDamaged)
			{
				DamageTtl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (DamageTtl < 0.0f)
				{
					IsDamaged = false;
					Renderable.ColorMask = Color.White;
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}
	}
}