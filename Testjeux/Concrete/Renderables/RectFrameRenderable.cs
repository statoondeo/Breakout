using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RectFrameRenderable : BaseRenderable
	{
		protected IBoxBody Body;

		public RectFrameRenderable(IBoxBody body)
		{
			Body = body;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			ServiceLocator.Instance.Get<ShapeFactory>().DrawRectangle(Color.YellowGreen, Body.Position.ToPoint(), Body.Size.ToPoint(), spriteBatch);
		}
	}
}