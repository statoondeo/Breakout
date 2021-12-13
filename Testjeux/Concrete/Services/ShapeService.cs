using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class ShapeService : IShapeService
	{
		protected SpriteBatch SpriteBatch;
		public ShapeService(SpriteBatch spriteBatch) 
		{
			SpriteBatch = spriteBatch;
		}

		public Texture2D CreateTexture(Point size, Color color)
		{
			Texture2D newTexture = new Texture2D(SpriteBatch.GraphicsDevice, size.X, size.Y);
			newTexture.SetData(Enumerable.Repeat(color, size.X * size.Y).ToArray());
			return (newTexture);
		}

		public Texture2D CropTexture(Texture2D original, Rectangle source)
		{
			Texture2D cropTexture = new Texture2D(SpriteBatch.GraphicsDevice, source.Width, source.Height);
			Color[] data = new Color[source.Width * source.Height];
			original.GetData(0, source, data, 0, data.Length);
			cropTexture.SetData(data);
			return (cropTexture);
		}

		public void DrawLine(Color color, Vector2 from, Vector2 to, SpriteBatch spriteBatch)
		{
			float hLength = to.X - from.X;
			float vLength = to.Y - from.Y;
			float angle = (float)Math.Atan2(vLength, hLength);
			spriteBatch.Draw(ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.Drawable), new Rectangle(from.ToPoint(), new Point((int)Math.Sqrt(hLength * hLength + vLength * vLength), 1)), null, color, angle, Vector2.Zero, SpriteEffects.None, 1.0f);
		}

		public void DrawRectangle(Color color, Point from, Point size, SpriteBatch spriteBatch)
		{
			Texture2D texture = ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.Drawable);
			spriteBatch.Draw(texture, new Rectangle(from, new Point(size.X, 1)), color);
			spriteBatch.Draw(texture, new Rectangle(new Point(from.X + size.X, from.Y), new Point(1, size.Y)), color);
			spriteBatch.Draw(texture, new Rectangle(from, new Point(1, size.Y)), color);
			spriteBatch.Draw(texture, new Rectangle(new Point(from.X, from.Y + size.Y), new Point(size.X, 1)), color);
		}

		public void DrawCircle(Color color, Vector2 center, int radius, int points, SpriteBatch spriteBatch)
		{
			float angleStep = (float)(2 * Math.PI / points);
			for (int i = 1; i < points; i++)
			{
				DrawLine(
					color,
					new Vector2(center.X + (float)Math.Cos(angleStep * (i - 1)) * radius, center.Y + (float)Math.Sin(angleStep * (i - 1)) * radius),
					new Vector2(center.X + (float)Math.Cos(angleStep * i) * radius, center.Y + (float)Math.Sin(angleStep * i) * radius),
					spriteBatch);
			}
		}
	}
}
