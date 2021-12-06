using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class TextureRenderable : IRenderable
	{
		protected Texture2D Texture;
		protected IGameObject GameObject;

		public TextureRenderable(IGameObject gameObject, Texture2D texture)
		{
			GameObject = gameObject;
			Texture = texture;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, GameObject.Body.Position, Color.White);
		}
	}
}