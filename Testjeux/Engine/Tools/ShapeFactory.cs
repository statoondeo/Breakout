using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class ShapeFactory
	{
		public Texture2D CreateTexture(Point size, Color color)
		{			
			Texture2D newTexture = new Texture2D(ServiceLocator.Instance.Get<SpriteBatch>().GraphicsDevice, size.X, size.Y);
			newTexture.SetData(Enumerable.Repeat(color, size.X * size.Y).ToArray());
			return (newTexture);
		}

		public void DrawLine(Color color, Point from, Point size, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(ServiceLocator.Instance.Get<AssetManager>().DrawableTexture, new Rectangle(from, size), color);
		}

		public void DrawRectangle(Color color, Point from, Point size, SpriteBatch spriteBatch)
		{
			Texture2D texture = ServiceLocator.Instance.Get<AssetManager>().DrawableTexture;
			spriteBatch.Draw(texture, new Rectangle(from, new Point(size.X, 1)), color);
			spriteBatch.Draw(texture, new Rectangle(new Point(from.X + size.X, from.Y), new Point(1, size.Y)), color);
			spriteBatch.Draw(texture, new Rectangle(from, new Point(1, size.Y)), color);
			spriteBatch.Draw(texture, new Rectangle(new Point(from.X, from.Y + size.Y), new Point(size.X, 1)), color);
		}
	}
}
