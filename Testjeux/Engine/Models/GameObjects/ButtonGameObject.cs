using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class ButtonGameObject : BaseGameObject
	{
		protected IRenderable TextRenderable;

		public ButtonGameObject(Vector2 position, Point size, Color color, SpriteFont spriteFont, string text, Color textColor, ICommand command)
		{
			Vector2 textSize = spriteFont.MeasureString(text);
			Movable = new ImmobileMovable(position, size);
			Collidable = new ButtonCollidable(Movable, command);
			TextRenderable = new TextRenderable(Movable,new Vector2((size.X - textSize.X) / 2, (size.Y - textSize.Y) / 2), spriteFont, text, textColor);
			Renderable = new TextureRenderable(Movable, ServiceLocator.Instance.Get<ShapeFactory>().CreateTexture(Movable.Size, color));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			TextRenderable.Draw(spriteBatch);
		}
	}
}