using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class TextGameObject : BaseGameObject
	{
		public TextGameObject(Vector2 position, Vector2 size, SpriteFont spriteFont, string text, Color textColor)
			: this(position, size, spriteFont, text, textColor, 0.0f)
		{
		}

		public TextGameObject(Vector2 position, Vector2 size, SpriteFont spriteFont, string text, Color textColor, float angle)
		{
			Body = new InvisibleBody(position);
			Renderable = new TextRenderable(this, Vector2.Zero, spriteFont, text, textColor)
			{
				Layer = 0.75f
			};
			Body.Angle = angle;
		}
	}
}