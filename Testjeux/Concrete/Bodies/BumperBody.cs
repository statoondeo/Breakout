using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BumperBody : CircleBody
	{
		protected IRenderable Renderable;

		public BumperBody(Vector2 position, float radius, IColliderCommand command) 
			: base(position, radius, Vector2.Zero, 0.5f, 1.0f, true, command)
		{
			Renderable = new CircleFrameRenderable(this, Vector2.Zero);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch);
		}
	}
}