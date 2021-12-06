using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BrickBody : BoxBody
	{
		protected IRenderable Renderable;

		public BrickBody(Vector2 position, Vector2 size, IColliderCommand command) 
			: base(position, size, Vector2.Zero, 1.0f, 1.0f, true, command)
		{
			Renderable = new RectFrameRenderable(this);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch);
		}
	}
}