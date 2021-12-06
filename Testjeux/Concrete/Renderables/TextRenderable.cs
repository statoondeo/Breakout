using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class TextRenderable : IRenderable
	{
		protected IGameObject GameObject;
		protected Vector2 Offset;
		protected SpriteFont SpriteFont;
		protected string Text;
		protected Color Color;

		public TextRenderable(IGameObject gameObject, Vector2 offset, SpriteFont spriteFont, string text, Color color)
		{
			GameObject = gameObject;
			Offset = offset;
			SpriteFont = spriteFont;
			Text = text;
			Color = color;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(SpriteFont, Text, GameObject.Body.Position + Offset, Color);
		}
	}
}