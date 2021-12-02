using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public interface IPositionable
	{
		Vector2 Position { get; set; }
		Point Size { get; set; }
	}
}