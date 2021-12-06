using System.Collections.Generic;
using System.Linq;
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
			// Gestion des updates des GameObjects
			foreach (IGameObject gameObject in GameObjectsCollection.Where(gameObject => gameObject.Status == GameObjectStatus.ACTIVE))
			{
				gameObject.Update(gameTime);
			}

			// Gestion des collisions des GameObjects, et uniquement ceux qui sont actifs et qui dispose d'une collideBox
			CollisionTestResult collisionResult = null;
			IGameObject goi = null;
			IGameObject goj = null;
			for (int i = 0; i < (GameObjectsCollection.Count - 1); i++)
			{
				goi = GameObjectsCollection[i];
				if (goi.Status == GameObjectStatus.ACTIVE && !(goi.Body is DummyBody))
				{
					for (int j = i + 1; j < GameObjectsCollection.Count; j++)
					{
						goj = GameObjectsCollection[j];
						if (goj.Status == GameObjectStatus.ACTIVE
							&& !(goj.Body is DummyBody)
							//&& goi.Partition != goj.Partition
							&& (goj.Body is IBoxBody || goj.Body is ICircleBody) 
							&& (collisionResult = BodyCollider.IsCollision(goi.Body, goj.Body)) != null)
						{
							BodyCollider.ResolveCollision(goi.Body, goj.Body, collisionResult);
							goi.Body.CollideCommand.Execute(goj, collisionResult);
							goj.Body.CollideCommand.Execute(goi, collisionResult);
						}
					}
				}
			}

			// On purge tous les éléments obsolètes
			(GameObjectsCollection as List<IGameObject>).RemoveAll(gameObjectStatus => gameObjectStatus.Status == GameObjectStatus.OUTDATED);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			// On n'affiche que les éléments actifs
			foreach (IGameObject gameObject in GameObjectsCollection.Where(gameObject => gameObject.Status == GameObjectStatus.ACTIVE))
			{
				gameObject.Draw(spriteBatch);
			}
		}
	}
}
