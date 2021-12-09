using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BumperBody : CircleBody
	{
		protected IRenderable Renderable;

		public BumperBody(Vector2 position, float radius) 
			: base(position, radius, Vector2.Zero, 0.5f, 1.0f, true, new DummyColliderCommand())
		{
			Renderable = new CircleFrameRenderable(this);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch);
		}
	}
}