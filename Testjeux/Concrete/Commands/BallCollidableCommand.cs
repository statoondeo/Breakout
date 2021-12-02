using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BallCollidableCommand : BaseCollidableCommand
	{
		protected VelocityMovable Movable;

		public BallCollidableCommand(VelocityMovable movable)
		{
			Movable = movable;
		}

		public override void Execute(ICollidable collidable)
		{
			// On teste la taille des chevauchements pour savoir si on touche sur un coté horizontal ou vertical
			float horizontalOverlap = MathHelper.Min(collidable.Positionable.Position.X + collidable.Positionable.Size.X, Movable.Position.X + Movable.Size.X) - MathHelper.Max(collidable.Positionable.Position.X, Movable.Position.X);
			float verticalOverlap = MathHelper.Min(collidable.Positionable.Position.Y + collidable.Positionable.Size.Y, Movable.Position.Y + Movable.Size.Y) - MathHelper.Max(collidable.Positionable.Position.Y, Movable.Position.Y);
			Movable.Velocity *= (horizontalOverlap - verticalOverlap) >= 0 ? new Vector2(1, -1) : new Vector2(-1, 1);
		}
	}
}