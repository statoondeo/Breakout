using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseGameObject : IGameObject
	{
		public IMovable Movable { get; set; }
		public IBody Body { get; set; }
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
			IGameObject ball = Services.Instance.Get<ISceneService>().GetObject(item => item is BallGameObject);
			Movable.Move(gameTime);
			Renderable.Update(gameTime);
		}

		public virtual void Draw(SpriteBatch spriteBatch) 
		{
			Renderable.Draw(spriteBatch);
			//Body.Draw(spriteBatch);
		}

		//public override string ToString()
		//{
		//	StringBuilder sb = new StringBuilder();
		//	sb.AppendLine(this.GetType().ToString() + "=>");
		//	sb.AppendLine(Body.ToString());
		//	return (sb.ToString());
		//}
	}
}