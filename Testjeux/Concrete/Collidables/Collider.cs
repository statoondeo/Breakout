using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public static class Collider
	{
		public static bool IsCollision(ICollidable collidable1, ICollidable collidable2)
		{
			switch (collidable1.Type)
			{
				case CollidableType.CIRCLE:
					return (IsCircleCollision(collidable1 as CircleCollidable, collidable2));
				case CollidableType.RECTANGLE:
					return (IsRectangleCollision(collidable1 as RectangleCollidable, collidable2));
				default:
					return (false);
			}
		}

		private static bool IsCircleCollision(CircleCollidable circle, ICollidable collidable)
		{
			switch (collidable.Type)
			{
				case CollidableType.CIRCLE:
					return (IsCircleIntersectsCircle(circle, collidable as CircleCollidable));
				case CollidableType.RECTANGLE:
					return (IsCircleIntersectsRectangle(circle, collidable as RectangleCollidable));
				default:
					return (false);
			}
		}

		private static bool IsRectangleCollision(RectangleCollidable rectangle, ICollidable collidable)
		{
			switch (collidable.Type)
			{
				case CollidableType.CIRCLE:
					return (IsCircleIntersectsRectangle(collidable as CircleCollidable, rectangle));
				case CollidableType.RECTANGLE:
					return (IsRectangleIntersectsRectangle(rectangle, collidable as RectangleCollidable));
				default:
					return (false);
			}
		}

		private static bool IsCircleIntersectsCircle(CircleCollidable circle1, CircleCollidable circle2)
		{
			int centerDistance = (circle1.Radius + circle2.Radius) * (circle1.Radius + circle2.Radius);
			float hDistance = (circle1.Positionable.Position.X - circle2.Positionable.Position.X) * (circle1.Positionable.Position.X - circle2.Positionable.Position.X);
			float vDistance = (circle1.Positionable.Position.Y - circle2.Positionable.Position.Y) * (circle1.Positionable.Position.Y - circle2.Positionable.Position.Y);
			return ((hDistance + vDistance) <= centerDistance);
		}

		private static bool IsRectangleIntersectsRectangle(RectangleCollidable rectangle1, RectangleCollidable rectangle2)
		{
			Rectangle box1 = new Rectangle(rectangle1.Positionable.Position.ToPoint(), rectangle1.Positionable.Size);
			Rectangle box2 = new Rectangle(rectangle2.Positionable.Position.ToPoint(), rectangle2.Positionable.Size);
			return (box1.Intersects(box2));
		}

		private static bool IsCircleIntersectsRectangle(CircleCollidable circle, RectangleCollidable rectangle)
		{
			float distX = Math.Abs(circle.Positionable.Position.X + circle.Radius - rectangle.Positionable.Position.X - rectangle.Positionable.Size.X / 2);
			float distY = Math.Abs(circle.Positionable.Position.Y + circle.Radius - rectangle.Positionable.Position.Y - rectangle.Positionable.Size.Y / 2);

			if (distX > (rectangle.Positionable.Size.X / 2 + circle.Radius)) return (false);
			if (distY > (rectangle.Positionable.Size.Y / 2 + circle.Radius)) return (false);

			if (distX <= (rectangle.Positionable.Size.X / 2)) return (true);
			if (distY <= (rectangle.Positionable.Size.Y / 2)) return (true);

			float dx = distX - rectangle.Positionable.Size.X / 2;
			float dy = distY - rectangle.Positionable.Size.Y / 2;
			return (dx * dx + dy * dy <= circle.Radius * circle.Radius);
		}
	}
}
