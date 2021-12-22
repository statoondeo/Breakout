using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public abstract class BaseMovable : IMovable
	{
		public IGameObject GameObject { get; set; }

		protected BaseMovable(IGameObject gameObject)
		{
			GameObject = gameObject;
		}

		public virtual void Move(GameTime gameTime) { }
	}
}