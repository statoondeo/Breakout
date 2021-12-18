using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class ScrollingBackgroundGameObject : BaseGameObject
	{
		protected float PositionLimit;

		public ScrollingBackgroundGameObject(Texture2D texture, Vector2 velocity, Vector2 startingPosition)
			: base()
		{
			Type = GameObjectType.NONE;
			Status = GameObjectStatus.ACTIVE;
			Body = new InvisibleBody(startingPosition, velocity);
			Movable = new VelocityMovable(this);
			PositionLimit = texture.Width;
			Renderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero)
			{
				Layer = 0.1f
			};
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (Body.Position.X < -PositionLimit)
			{
				Body.MoveTo(new Vector2(PositionLimit, Body.Position.Y));
			}
		}
	}
}