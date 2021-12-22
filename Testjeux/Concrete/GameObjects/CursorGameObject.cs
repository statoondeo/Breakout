using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class CursorGameObject : BaseGameObject
	{
		protected IRenderable HaloRenderable;

		public CursorGameObject()
		{
			Type = GameObjectType.CURSOR;
			Movable = new MouseMovable(this);
			Body = new BoxBody(Vector2.Zero, Vector2.One, Vector2.Zero, 1.0f, false, new DummyColliderCommand());
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
			HaloRenderable = new TextureRenderable(this, texture, 1.0f, 0.5f * (Vector2.One - 1.0f * (new Vector2(texture.Width, texture.Height))))
			{
				Alpha = 0.15f
			};
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			HaloRenderable.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}
	}
}