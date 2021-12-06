using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class TextGameObject : BaseGameObject
	{
		public TextGameObject(Vector2 position, Vector2 size, SpriteFont spriteFont, string text, Color textColor)
		{
			Body = new BoxBody(position, size, Vector2.Zero, 0.0f, 1.0f, true, new DummyColliderCommand());
			Renderable = new TextRenderable(this, Vector2.Zero, spriteFont, text, textColor);
		}
	}
}