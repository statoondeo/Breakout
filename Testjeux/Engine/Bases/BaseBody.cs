using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseBody : IBody
	{
		protected BaseBody(Vector2 position, Vector2 velocity, float restitution, bool isStatic, IColliderCommand command)
		{
			Position = position;
			Velocity = velocity;
			Restitution = restitution;
			IsStatic = isStatic;
			CollideCommand = command;
			Force = Vector2.Zero;
			Angle = 0.0f;
		}

		//public override string ToString()
		//{
		//	StringBuilder sb = new StringBuilder();
		//	sb.AppendLine("Body =>");
		//	sb.AppendLine("\tPosition=" + Position.ToString());
		//	sb.AppendLine("\tVelocity=" + Velocity.ToString());
		//	sb.AppendLine("\tForce=" + Force.ToString());
		//	sb.AppendLine("\tRestitution=" + Restitution.ToString());
		//	sb.AppendLine("\tIsStatic=" + IsStatic.ToString());
		//	sb.AppendLine("\tAngle=" + Angle.ToString());
		//	sb.AppendLine("\tRotationOrigin=" + RotationOrigin.ToString());
		//	return (sb.ToString());
		//}

		public virtual Vector2 Force { get; set; }
		public virtual Vector2 Position { get; protected set; }
		private Vector2 mVelocity;
		public virtual Vector2 Velocity 
		{
			get => mVelocity;
			set
			{
				mVelocity = IsStatic ? Vector2.Zero : value;
			}
		}
		public virtual float Restitution { get; protected set; }
		public virtual bool IsStatic { get; set; }
		public virtual IColliderCommand CollideCommand { get; set; }
		public float Angle { get; set; }
		public Vector2 RotationOrigin { get; set; }

		public virtual void Move(Vector2 offset) => Position += offset;
		public virtual void MoveTo(Vector2 position) => Position = position;
		public virtual void Draw(SpriteBatch spriteBatch) { }
	}
}
