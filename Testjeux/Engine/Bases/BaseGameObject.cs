using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseGameObject : IGameObject
	{
		public IMovable Movable { get; set; }
		public IBody Body { get; protected set; }
		public IRenderable Renderable { get; set; }
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
			Renderable.Update(gameTime);
		}
		public virtual void Draw(SpriteBatch spriteBatch) 
		{
			Renderable.Draw(spriteBatch);
			//Body.Draw(spriteBatch);
		}
	}
}