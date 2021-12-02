using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseGameObject : IGameObject
	{
		public IMovable Movable { get; protected set; }
		public ICollidable Collidable { get; protected set; }
		public IRenderable Renderable { get; protected set; }
		public GameObjectStatus Status { get; set; }
		public GameObjectType Type { get; protected set; }

		protected BaseGameObject()
		{
			Status = GameObjectStatus.ACTIVE;
			Type = GameObjectType.NONE;
			Movable = new DummyMovable();
			Collidable = new DummyCollidable();
			Renderable = new DummyRenderable();
		}

		public virtual void Update(GameTime gameTime) 
		{
			Movable.Move(gameTime);
			Collidable.Collide();
		}
		public virtual void Draw(SpriteBatch spriteBatch) 
		{
			Renderable.Draw(spriteBatch);
			Collidable.Draw(spriteBatch);
		}
	}
}