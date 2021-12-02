using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class TextureRenderable : IRenderable
	{
		protected IPositionable Positionable;
		protected Texture2D Texture;

		public TextureRenderable(IPositionable positionable, Texture2D texture)
		{
			Positionable = positionable;
			Texture = texture;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Positionable.Position, Color.White);
		}
	}
}