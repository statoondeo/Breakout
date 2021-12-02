using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseCollidable : ICollidable
	{
		public IPositionable Positionable { get; protected set; }
		public CollidableType Type { get; protected set; }

		protected BaseCollidable(IPositionable positionable) 
		{
			Positionable = positionable;
		}

		public virtual void Collide() { }

		public virtual void Draw(SpriteBatch spriteBatch) { }
	}
}