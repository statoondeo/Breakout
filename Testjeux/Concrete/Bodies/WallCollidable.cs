using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class WallBody : BoxBody
	{
		protected IRenderable Renderable;

		public WallBody(Vector2 position, Vector2 size, IColliderCommand command) 
			: base(position, size, Vector2.Zero, 0.0f, 1.0f, true, command)
		{
			Renderable = new RectFrameRenderable(this);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch);
		}
	}
}