using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BallBody : CircleBody
	{
		protected IRenderable Renderable;

		public BallBody(Vector2 position, Vector2 size, Vector2 velocity, IColliderCommand command) 
			: base(position, size.X / 2, velocity, 1.0f, false, command)
		{
			Force = new Vector2(0.0f, 4.0f);
			Renderable = new CircleFrameRenderable(this, Vector2.Zero);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch);
		}
	}
}