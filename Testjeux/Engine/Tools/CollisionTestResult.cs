using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CollisionTestResult
	{
		public IBody BodyA { get; protected set; }
		public IBody BodyB { get; protected set; }
		public Vector2 Normal { get; protected set; }
		public float Depth { get; protected set; }

		public CollisionTestResult(IBody bodyA, IBody bodyB, Vector2 normal, float depth)
		{
			BodyA = bodyA;
			BodyB = bodyB;
			Normal = normal;
			Depth = depth;
		}
	}
}
