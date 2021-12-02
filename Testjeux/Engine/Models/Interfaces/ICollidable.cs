using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface ICollidable
	{
		IPositionable Positionable { get; }
		CollidableType Type { get; }
		void Collide();

		void Draw(SpriteBatch spriteBatch);
	}
}