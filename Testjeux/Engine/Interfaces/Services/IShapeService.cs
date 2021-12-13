using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IShapeService : IService
	{
		Texture2D CreateTexture(Point size, Color color);
		void DrawCircle(Color color, Vector2 center, int radius, int points, SpriteBatch spriteBatch);
		void DrawLine(Color color, Vector2 from, Vector2 to, SpriteBatch spriteBatch);
		void DrawRectangle(Color color, Point from, Point size, SpriteBatch spriteBatch);
		Texture2D CropTexture(Texture2D original, Rectangle source);
	}
}

