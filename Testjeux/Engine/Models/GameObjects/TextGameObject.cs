using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class TextGameObject : BaseGameObject
	{
		public TextGameObject(Vector2 position, Point size, SpriteFont spriteFont, string text, Color textColor)
		{
			Movable = new ImmobileMovable(position, size);
			Renderable = new TextRenderable(Movable, Vector2.Zero, spriteFont, text, textColor);
		}
	}
}