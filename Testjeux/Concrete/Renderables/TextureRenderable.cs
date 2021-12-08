using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class TextureRenderable : BaseRenderable
	{
		public Texture2D Texture { get; set; }
		protected IGameObject GameObject;

		public TextureRenderable(IGameObject gameObject, Texture2D texture)
		{
			GameObject = gameObject;
			Texture = texture;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, GameObject.Body.Position, Color.White);
		}

		public override void Draw(SpriteBatch spriteBatch, float alpha, float angle, float scale, Vector2 rotationOrigin)
		{
			spriteBatch.Draw(Texture, GameObject.Body.Position, null, Color.White * alpha, angle, rotationOrigin, scale, SpriteEffects.None, 1.0f);
		}
	}
}