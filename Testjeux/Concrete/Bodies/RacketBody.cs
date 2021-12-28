using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RacketBody : BaseCompositeBody
	{
		protected static readonly float RESOLVER_RADIUS = 200;

		protected IRenderable RacketRenderable;
		protected IRenderable ControllerRenderable;

		public RacketBody(Vector2 position, Vector2 size, IColliderCommand command)
			: base(
				  new BoxBody(position, size, Vector2.Zero, 1.0f, true, command),
				  new CircleBody(position, RESOLVER_RADIUS, Vector2.Zero, 1.0f, true, new DummyColliderCommand()))
		{
			CollisionResolverOffset = new Vector2(size.X / 2 - RESOLVER_RADIUS, -2);
			RacketRenderable = new RectFrameRenderable(CollisionCheckerBody as IBoxBody);
			ControllerRenderable = new CircleFrameRenderable(CollisionResolverBody as ICircleBody, Vector2.Zero);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			RacketRenderable.Draw(spriteBatch);
			ControllerRenderable.Draw(spriteBatch);
		}
	}
}