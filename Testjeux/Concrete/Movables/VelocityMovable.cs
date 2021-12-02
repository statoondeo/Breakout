using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class VelocityMovable : BaseMovable
	{
		public Vector2 Velocity { get; set; }

		public VelocityMovable(Vector2 position, Point size, Vector2 velocity)
			: base(position, size)
		{
			Velocity = velocity;
		}

		public override void Move(GameTime gameTime)
		{
			base.Move(gameTime);
			Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
		}
	}
}