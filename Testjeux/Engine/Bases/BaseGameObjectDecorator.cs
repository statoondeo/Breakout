using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseGameObjectDecorator : IGameObjectDecorator
	{
		public IGameObject DecoratedGameObject { get; protected set; }

		protected BaseGameObjectDecorator(IGameObject gameObject)
		{
			DecoratedGameObject = gameObject;
		}

		public virtual GameObjectStatus Status { get => DecoratedGameObject.Status; set => DecoratedGameObject.Status = value; }

		public virtual GameObjectType Type => DecoratedGameObject.Type;

		public virtual IMovable Movable { get => DecoratedGameObject.Movable; set => DecoratedGameObject.Movable = value; }

		public virtual IBody Body { get => DecoratedGameObject.Body; set => DecoratedGameObject.Body = value; }

		public virtual IRenderable Renderable { get => DecoratedGameObject.Renderable; set => DecoratedGameObject.Renderable = value; }

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			DecoratedGameObject.Draw(spriteBatch);
		}

		public virtual void Update(GameTime gameTime)
		{
			DecoratedGameObject.Update(gameTime);
		}
	}
}