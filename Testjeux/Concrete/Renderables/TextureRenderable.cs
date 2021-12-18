using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class TextureRenderable : BaseRenderable
	{
		public Texture2D Texture { get; set; }
		public IGameObject GameObject { get; set; }

		public TextureRenderable(IGameObject gameObject, Texture2D texture, float scale, Vector2 offset)
			: base(offset, scale)
		{
			GameObject = gameObject;
			Texture = texture;
			Layer = 0.5f;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, GameObject.Body.Position + Offset, null, ColorMask * Alpha, GameObject.Body.Angle, GameObject.Body.RotationOrigin, Scale, SpriteEffects.None, Layer);
		}

		public override void Draw(SpriteBatch spriteBatch, float alpha, float angle, float scale, Vector2 rotationOrigin)
		{
			spriteBatch.Draw(Texture, GameObject.Body.Position + Offset, null, ColorMask * alpha, angle, rotationOrigin, scale, SpriteEffects.None, Layer);
		}
	}
}