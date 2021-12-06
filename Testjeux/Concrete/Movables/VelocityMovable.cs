using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class VelocityMovable : BaseMovable
	{
		public VelocityMovable(IGameObject gameObject)
			: base(gameObject)
		{ }

		public override void Move(GameTime gameTime)
		{
			base.Move(gameTime);
			GameObject.Body.Velocity += GameObject.Body.Force;
			GameObject.Body.Move(GameObject.Body.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
		}
	}
}