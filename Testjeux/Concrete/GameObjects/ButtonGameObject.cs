using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class ButtonGameObject : BaseGameObject
	{
		public static readonly Vector2 Size = new Vector2(299, 93);

		protected IRenderable TextRenderable;

		public ButtonGameObject(Vector2 position, string text, Color textColor, ICommand command)
		{
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.Button);
			Vector2 size = new Vector2(texture.Width, texture.Height);
			Type = GameObjectType.BUTTON;
			SpriteFont font = Services.Instance.Get<IAssetService>().GetFont(FontName.Button);
			Vector2 textSize = font.MeasureString(text);
			Body = new BoxBody(position, size, Vector2.Zero, 1.0f, true, new ButtonWrapperColliderCommand(this, command));
			TextRenderable = new TextRenderable(this, new Vector2((size.X - textSize.X) / 2, (size.Y - textSize.Y) / 2 - 10), font, text, textColor);
			Renderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			TextRenderable.Draw(spriteBatch);
		}
	}
}