using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public abstract class BaseMovable : BasePositionable, IMovable
	{
		protected BaseMovable(Vector2 position, Point size) : base(position, size) { }

		public virtual void Move(GameTime gameTime) { }
	}
}