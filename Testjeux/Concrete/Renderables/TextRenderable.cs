using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class TextRenderable : BaseRenderable
	{
		protected IGameObject GameObject;
		protected SpriteFont SpriteFont;
		protected string Text;
		protected Color Color;

		public TextRenderable(IGameObject gameObject, Vector2 offset, SpriteFont spriteFont, string text, Color color)
			: base(offset, 1.0f)
		{
			GameObject = gameObject;
			SpriteFont = spriteFont;
			Text = text;
			Color = color;
			Layer = 0.9f;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(SpriteFont, Text, GameObject.Body.Position + Offset, Color * Alpha, GameObject.Body.Angle, GameObject.Body.RotationOrigin, 1.0f, SpriteEffects.None, Layer);
		}

		public override void Draw(SpriteBatch spriteBatch, float alpha, float angle, float scale, Vector2 rotationOrigin)
		{
			spriteBatch.DrawString(SpriteFont, Text, GameObject.Body.Position + Offset, Color * alpha, angle, rotationOrigin, scale, SpriteEffects.None, Layer);
		}
	}
}