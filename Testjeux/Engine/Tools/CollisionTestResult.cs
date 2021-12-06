using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CollisionTestResult
	{
		public Vector2 Normal { get; protected set; }
		public float Depth { get; protected set; }

		public CollisionTestResult(Vector2 normal, float depth)
		{
			Normal = normal;
			Depth = depth;
		}
	}
}
