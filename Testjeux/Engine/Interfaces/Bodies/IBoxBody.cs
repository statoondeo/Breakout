using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public interface IBoxBody : IBody
	{
		Vector2 Size { get; }
		Vector2[] Vectors { get; }
	}
}
