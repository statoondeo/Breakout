using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public interface IMovable
	{
		IGameObject GameObject { get; set; }
		void Move(GameTime gameTime);
	}
}