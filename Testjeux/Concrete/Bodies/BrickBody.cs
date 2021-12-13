using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BrickBody : CircleBody
	{
		protected IRenderable Renderable;

		public BrickBody(Vector2 position, float radius, IColliderCommand command) 
			: base(position, radius, Vector2.Zero, 1.0f, 0.9f, true, command)
		{
			Renderable = new CircleFrameRenderable(this, Vector2.Zero);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch);
		}
	}
}