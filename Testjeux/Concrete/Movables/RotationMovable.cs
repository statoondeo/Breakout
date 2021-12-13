using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class RotationMovable : BaseMovable
	{
		protected Vector2 Center;
		protected float CurrentAngle;
		protected float AngleSpeed;
		protected float Radius;

		public RotationMovable(IGameObject gameObject, Vector2 center, float radius, float currentAngle, float angleSpeed)
			: base(gameObject)
		{
			Center = center;
			CurrentAngle = currentAngle;
			AngleSpeed = angleSpeed;
			Radius = radius;
		}

		public override void Move(GameTime gameTime)
		{
			base.Move(gameTime);
			CurrentAngle += AngleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
			GameObject.Body.MoveTo(new Vector2(Center.X + (float)Math.Cos(CurrentAngle) * Radius, Center.Y + (float)Math.Sin(CurrentAngle) * Radius));
		}
	}
}