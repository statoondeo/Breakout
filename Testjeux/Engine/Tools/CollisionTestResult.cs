using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CollisionTestResult
	{
		public IBody BodyA { get; set; }
		public IBody BodyB { get; set; }
		public Vector2 Normal { get; set; }
		public float Depth { get; set; }

		public CollisionTestResult(IBody bodyA, IBody bodyB, Vector2 normal, float depth)
		{
			BodyA = bodyA;
			BodyB = bodyB;
			Normal = normal;
			Depth = depth;
		}
	}
}
