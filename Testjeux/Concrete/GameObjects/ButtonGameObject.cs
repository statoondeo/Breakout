using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class ButtonGameObject : BaseGameObject
	{
		protected IRenderable TextRenderable;

		public ButtonGameObject(Vector2 position, Vector2 size, Color color, SpriteFont spriteFont, string text, Color textColor, ICommand command)
		{
			Type = GameObjectType.BUTTON;
			Vector2 textSize = spriteFont.MeasureString(text);
			Body = new BoxBody(position, size, Vector2.Zero, 0.0f, 1.0f, true, new ButtonWrapperColliderCommand(this, command));
			TextRenderable = new TextRenderable(this, new Vector2((size.X - textSize.X) / 2, (size.Y - textSize.Y) / 2), spriteFont, text, textColor);
			Renderable = new TextureRenderable(this, ServiceLocator.Instance.Get<IShapeService>().CreateTexture(size.ToPoint(), color));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			TextRenderable.Draw(spriteBatch);
		}
	}
}