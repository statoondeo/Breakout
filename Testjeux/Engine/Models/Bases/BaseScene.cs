using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseScene : IScene
	{
		public IList<IGameObject> GameObjectsCollection { get; protected set; }
		protected ICommand CommandWhenUnloaded;

		protected BaseScene() 
		{
			GameObjectsCollection = new List<IGameObject>();
		}

		public virtual void Load() { }

		public virtual void UnLoad(ICommand commandWhenUnloaded) 
		{
			CommandWhenUnloaded = commandWhenUnloaded;
			CommandWhenUnloaded.Execute();
		}

		public virtual IGameObject RegisterGameObject(IGameObject gameObject)
		{
			GameObjectsCollection.Add(gameObject);
			return (gameObject);
		}

		public virtual IGameObject UnRegisterGameObject(IGameObject gameObject)
		{
			GameObjectsCollection.Remove(gameObject);
			return (gameObject);
		}

		public virtual void Update(GameTime gameTime)
		{
			foreach (IGameObject gameObject in GameObjectsCollection)
			{
				gameObject.Update(gameTime);
			}
			(GameObjectsCollection as List<IGameObject>).RemoveAll(gameObjectStatus => gameObjectStatus.Status == GameObjectStatus.OUTDATED);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			foreach (IGameObject gameObject in GameObjectsCollection)
			{
				gameObject.Draw(spriteBatch);
			}
		}
	}
}
