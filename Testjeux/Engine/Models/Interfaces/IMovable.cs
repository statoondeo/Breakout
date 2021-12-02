using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public interface IMovable : IPositionable
	{
		void Move(GameTime gameTime);
	}
}