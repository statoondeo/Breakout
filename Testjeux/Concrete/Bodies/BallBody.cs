using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BallBody : CircleBody
	{
		protected IRenderable Renderable;

		public BallBody(Vector2 position, Vector2 size, Vector2 velocity, IColliderCommand command) 
			: base(position, size.X / 2, velocity, 0.5f, 1.0f, false, command)
		{
			//Force = new Vector2(0, 5f);
			Renderable = new CircleFrameRenderable(this);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch);
		}
	}
}