using System.Text;
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
		public float Restitution => CollisionCheckerBody.Restitution;
		public bool IsStatic { get => CollisionCheckerBody.IsStatic; set => CollisionCheckerBody.IsStatic = value; }
		public IColliderCommand CollideCommand { get => CollisionCheckerBody.CollideCommand; set => CollisionCheckerBody.CollideCommand = value; }
		public float Angle { get => CollisionCheckerBody.Angle; set => CollisionCheckerBody.Angle = value; }
		public Vector2 RotationOrigin { get => CollisionCheckerBody.RotationOrigin; set => CollisionCheckerBody.RotationOrigin = value; }

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

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("CollisionCheckerBody =>");
			sb.AppendLine(CollisionCheckerBody.ToString());
			sb.AppendLine(CollisionResolverBody.ToString());

			return (sb.ToString());
		}
	}
}
