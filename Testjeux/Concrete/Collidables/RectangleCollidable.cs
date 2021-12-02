using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RectangleCollidable : BaseCollidable
	{
		public RectangleCollidable(IPositionable positionable) 
			: base(positionable)
		{
			Type = CollidableType.RECTANGLE;
			Renderable = new RectFrameRenderable(Positionable);
		}
	}
}