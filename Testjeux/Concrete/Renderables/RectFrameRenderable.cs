using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RectFrameRenderable : IRenderable
	{
		protected IBoxBody Body;

		public RectFrameRenderable(IBoxBody body)
		{
			Body = body;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			ServiceLocator.Instance.Get<ShapeFactory>().DrawRectangle(Color.YellowGreen, Body.Position.ToPoint(), Body.Size.ToPoint(), spriteBatch);
		}
	}
}