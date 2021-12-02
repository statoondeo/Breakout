using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class FrameRenderable : IRenderable
	{
		protected IPositionable Positionable;

		public FrameRenderable(IPositionable positionable)
		{
			Positionable = positionable;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			ServiceLocator.Instance.Get<ShapeFactory>().DrawRectangle(Color.YellowGreen, Positionable.Position.ToPoint(), Positionable.Size, spriteBatch);
		}
	}
}