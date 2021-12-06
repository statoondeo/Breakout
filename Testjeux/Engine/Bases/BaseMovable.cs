using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public abstract class BaseMovable : IMovable
	{
		protected IGameObject GameObject;

		protected BaseMovable(IGameObject gameObject)
		{
			GameObject = gameObject;
		}

		public virtual void Move(GameTime gameTime) { }
	}
}