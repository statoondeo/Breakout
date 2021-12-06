using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CircleBody : BaseBody, ICircleBody
	{
		protected Vector2 CenterOffset;
		protected Vector2 OriginOffset;

		public CircleBody(Vector2 position, float radius, Vector2 velocity, float mass, float restitution, bool isStatic, IColliderCommand command)
			: base(position, velocity, mass, restitution, isStatic, command)
		{
			Radius = radius;
			CenterOffset = new Vector2(Radius);
			OriginOffset = Vector2.Zero;
		}

		public float Radius { get; private set; }

		public Vector2 Center { get => Position + CenterOffset; }

		public override Vector2 Position { get => base.Position + OriginOffset; protected set => base.Position = value; }
	}
}
