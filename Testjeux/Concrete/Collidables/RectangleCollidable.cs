using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RectangleCollidable : BaseCollidable
	{
		protected IRenderable Renderable;

		public RectangleCollidable(IPositionable positionable) 
			: base(positionable)
		{
			Type = CollidableType.RECTANGLE;
			Renderable = new FrameRenderable(Positionable);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			Renderable.Draw(spriteBatch);
		}
	}
}