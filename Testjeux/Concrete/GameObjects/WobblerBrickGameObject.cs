using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class WobblerBrickGameObject : BaseBrickGameObject
	{
		private readonly IRenderable SparkRenderable;
		private readonly IRenderable HaloRenderable;

		public WobblerBrickGameObject(Vector2 position, float scale)
			: base(position, 32, 1)
		{
			Renderable = new WobblerAnimatedTextureRenderable(this, scale);
			SparkRenderable = new AnimatedTextureRenderable(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.BlueSpark), new Vector2(-43), new Point(100), 1.5f, new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1 }, 0.09f, true));
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
			HaloRenderable = new TextureRenderable(this, texture, 1.5f, 0.5f * (new Vector2(64) - 1.5f * (new Vector2(texture.Width, texture.Height))))
			{
				Alpha = 0.25f,
				ColorMask = Color.CornflowerBlue
			};
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
}