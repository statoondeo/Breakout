using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class RockWallGameObject : WallGameObject
	{
		public RockWallGameObject(Vector2 position)
			: base(position, new Vector2(64))
		{
			int textureIndex = ServiceLocator.Instance.Get<IRandomService>().Next(0, 5);
			Texture2D texture = ServiceLocator.Instance.Get<IShapeService>().CropTexture(ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.Rocks), new Rectangle(new Point(textureIndex * 64, 0), new Point(64)));
			Renderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero);
		}
	}
	public sealed class WobblerBrickGameObject : BrickGameObject
	{
		public WobblerBrickGameObject(Vector2 position, float scale)
			: base(position, 32, 1)
		{
			Renderable = new WobblerAnimatedTextureRenderable(this, scale);
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

		public BrainBrickGameObject(Vector2 position, float scale)
			: base(position, TextureSize.X * 0.5f * scale * BodySizeFactor, 6)
		{
			Vector2 offset = (TextureSize * BodySizeFactor - TextureSize) * 0.5f + new Vector2(-5, 20);
			Body = new BrickBody(position, TextureSize.X * 0.5f * scale * BodySizeFactor, new BrainColliderCommand(this, new BrickExplosionParticlesEmitter(this, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 25)));
			Renderable = new BrainAnimatedTextureRenderable(this, scale, offset);
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
			Body = new BrickBody(position, radius, new BrickColliderCommand(this, new BrickExplosionParticlesEmitter(this, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 15)));
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