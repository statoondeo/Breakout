using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class CircleFrameRenderable : IRenderable
	{
		protected ICircleBody Body;

		public CircleFrameRenderable(ICircleBody body)
		{
			Body = body;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			ServiceLocator.Instance.Get<ShapeFactory>().DrawCircle(Color.YellowGreen, Body.Center, (int)Body.Radius, 50, spriteBatch);
		}
	}
}