using System.Collections.Concurrent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseGameObject : IGameObject
	{
		public IMovable Movable { get; protected set; }
		public IBody Body { get; protected set; }
		public IRenderable Renderable { get; protected set; }
		public GameObjectStatus Status { get; set; }
		public GameObjectType Type { get; protected set; }

		protected BaseGameObject()
		{
			Status = GameObjectStatus.ACTIVE;
			Type = GameObjectType.NONE;
			Movable = new DummyMovable();
			Body = new DummyBody();
			Renderable = new DummyRenderable();
		}

		public virtual void Update(GameTime gameTime) 
		{
			Movable.Move(gameTime);
		}
		public virtual void Draw(SpriteBatch spriteBatch) 
		{
			Renderable.Draw(spriteBatch);
			Body.Draw(spriteBatch);
		}
	}
}