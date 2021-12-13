using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class RevolutionMovable : BaseMovable
	{
		protected float AngleSpeed;

		public RevolutionMovable(IGameObject gameObject, Vector2 rotationOrigin, float currentAngle, float angleSpeed)
			: base(gameObject)
		{
			GameObject.Body.RotationOrigin = rotationOrigin;
			AngleSpeed = angleSpeed;
		}

		public override void Move(GameTime gameTime)
		{
			base.Move(gameTime);
			GameObject.Body.Angle += AngleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
		}
	}
}