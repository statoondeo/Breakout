using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class HaloGameObject : BaseGameObject
	{
		private readonly float AngleSpeed;

		public HaloGameObject(Color colorMask, float angleSpeed = 0.0f, float scale = 1.0f)
			: base()
		{
			AngleSpeed = angleSpeed;
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
			Renderable = new TextureRenderable(this, texture, scale, Vector2.Zero)
			{
				ColorMask = colorMask,
				Layer = 0.4f
			};
			Body = new InvisibleBody(Vector2.Zero)
			{
				RotationOrigin = texture.Bounds.Size.ToVector2() * 0.5f
			};
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			Body.Angle += (float)gameTime.ElapsedGameTime.TotalSeconds * AngleSpeed;
		}
	}
}