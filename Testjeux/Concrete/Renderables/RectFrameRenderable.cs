using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RectFrameRenderable : IRenderable
	{
		protected IPositionable Positionable;

		public RectFrameRenderable(IPositionable positionable)
		{
			Positionable = positionable;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			ShapeFactory shapes = ServiceLocator.Instance.Get<ShapeFactory>();
			shapes.DrawRectangle(Color.YellowGreen, Positionable.Position.ToPoint(), Positionable.Size, spriteBatch);
		}
	}
}