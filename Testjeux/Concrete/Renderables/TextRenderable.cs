using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class TextRenderable : IRenderable
	{
		protected IPositionable Positionable;
		protected Vector2 Offset;
		protected SpriteFont SpriteFont;
		protected string Text;
		protected Color Color;

		public TextRenderable(IPositionable positionable, Vector2 offset, SpriteFont spriteFont, string text, Color color)
		{
			Positionable = positionable;
			Offset = offset;
			SpriteFont = spriteFont;
			Text = text;
			Color = color;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(SpriteFont, Text, Positionable.Position + Offset, Color);
		}
	}
}