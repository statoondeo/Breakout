using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class CircleFrameRenderable : BaseRenderable
	{
		protected ICircleBody Body;

		public CircleFrameRenderable(ICircleBody body, Vector2 offset)
			: base(offset, 1.0f)
		{
			Body = body;
			Layer = 1.0f;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Services.Instance.Get<IShapeService>().DrawCircle(Color.YellowGreen, Body.Center, (int)Body.Radius, 50, spriteBatch);
		}
	}
}