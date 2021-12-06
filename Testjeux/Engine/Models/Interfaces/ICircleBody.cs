using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public interface ICircleBody : IBody
	{
		float Radius { get; }
		Vector2 Center { get; }
	}
}
