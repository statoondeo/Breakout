using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class CircleFrameRenderable : BaseRenderable
	{
		protected ICircleBody Body;

		public CircleFrameRenderable(ICircleBody body)
		{
			Body = body;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			ServiceLocator.Instance.Get<IShapeService>().DrawCircle(Color.YellowGreen, Body.Center, (int)Body.Radius, 50, spriteBatch);
		}
	}
}