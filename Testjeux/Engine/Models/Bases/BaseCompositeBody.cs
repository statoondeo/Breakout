using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseCompositeBody : ICompositeIntersecBody
	{
		protected BaseCompositeBody(IBody collisionCheckerBody, IBody collisionResolverBody)
		{
			CollisionCheckerBody = collisionCheckerBody;
			CollisionResolverBody = collisionResolverBody;
			CollisionResolverOffset = Vector2.Zero;
		}
		protected Vector2 CollisionResolverOffset;
		public  IBody CollisionCheckerBody { get; protected set; }
		public IBody CollisionResolverBody { get; protected set; }
		public Vector2 Force { get => CollisionCheckerBody.Force; set => CollisionCheckerBody.Force = value; }

		public Vector2 Position => CollisionCheckerBody.Position;

		public Vector2 Velocity { get => CollisionCheckerBody.Velocity; set => CollisionCheckerBody.Velocity = value; }

		public float Mass => CollisionCheckerBody.Mass;

		public float InvMass => CollisionCheckerBody.InvMass;

		public float Restitution => CollisionCheckerBody.Restitution;

		public bool IsStatic => CollisionCheckerBody.IsStatic;

		public IColliderCommand CollideCommand => CollisionCheckerBody.CollideCommand;

		public void Move(Vector2 offset)
		{
			CollisionCheckerBody.Move(offset);
			CollisionResolverBody.Move(offset);
		}

		public void MoveTo(Vector2 newPosition)
		{
			CollisionCheckerBody.MoveTo(newPosition);
			CollisionResolverBody.MoveTo(newPosition + CollisionResolverOffset);
		}

		public virtual void Draw(SpriteBatch spriteBatch) { }
	}
}
