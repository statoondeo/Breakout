using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class MouseMovable : BaseMovable
	{
		public MouseMovable(Vector2 position, Point size) : base(position, size) { }

		public override void Move(GameTime gameTime)
		{
			base.Move(gameTime);
			Position = new Vector2(
						MathHelper.Clamp(
							ServiceLocator.Instance.Get<InputListener>().MousePosition().X - Size.X / 2, 
							0, 
							ServiceLocator.Instance.Get<Game>().Window.ClientBounds.Width - Size.X), 
						Position.Y);
		}
	}
}