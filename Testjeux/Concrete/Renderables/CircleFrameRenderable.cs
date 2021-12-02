using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class CircleFrameRenderable : IRenderable
	{
		protected IPositionable Positionable;

		public CircleFrameRenderable(IPositionable positionable)
		{
			Positionable = positionable;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			ShapeFactory shapes = ServiceLocator.Instance.Get<ShapeFactory>();
			shapes.DrawCircle(Color.YellowGreen, Positionable.Position + Positionable.Size.ToVector2() * 0.5f, Positionable.Size.X / 2, 20, spriteBatch);
		}
	}
}