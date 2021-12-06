﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RacketBody : BaseCompositeBody
	{
		protected static readonly float RESOLVER_RADIUS = 250;

		protected IRenderable RacketRenderable;
		protected IRenderable ControllerRenderable;

		public RacketBody(Vector2 position, Vector2 size)
			: base(
				  new BoxBody(position, size, Vector2.Zero, 0.0f, 1.0f, true, new DummyColliderCommand()),
				  new CircleBody(position, RESOLVER_RADIUS, Vector2.Zero, 0.0f, 1.0f, true, new DummyColliderCommand()))
		{
			CollisionResolverOffset = new Vector2(size.X / 2 - RESOLVER_RADIUS, 0);
			RacketRenderable = new RectFrameRenderable(CollisionCheckerBody as IBoxBody);
			ControllerRenderable = new CircleFrameRenderable(CollisionResolverBody as ICircleBody);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			RacketRenderable.Draw(spriteBatch);
			ControllerRenderable.Draw(spriteBatch);
		}
	}
}