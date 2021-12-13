using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseBody : IBody
	{
		protected BaseBody(Vector2 position, Vector2 velocity, float mass, float restitution, bool isStatic, IColliderCommand command)
		{
			Position = position;
			Velocity = velocity;
			Mass = mass;
			Restitution = restitution;
			IsStatic = isStatic;
			InvMass = IsStatic ? 0 : 1 / Mass;
			CollideCommand = command;
			Force = Vector2.Zero;
			Angle = 0.0f;
		}

		public virtual Vector2 Force { get; set; }
		public virtual Vector2 Position { get; protected set; }
		public virtual Vector2 Velocity { get; set; }
		public virtual float Mass { get; protected set; }
		public virtual float InvMass { get; protected set; }
		public virtual float Restitution { get; protected set; }
		public virtual bool IsStatic { get; protected set; }
		public virtual IColliderCommand CollideCommand { get; protected set; }
		public float Angle { get; set; }
		public Vector2 RotationOrigin { get; set; }

		public virtual void Move(Vector2 offset) => Position += offset;
		public virtual void MoveTo(Vector2 position) => Position = position;
		public virtual void Draw(SpriteBatch spriteBatch) { }
	}
}
