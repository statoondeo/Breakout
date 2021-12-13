using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseScene : IScene
	{
		protected IList<IGameObject> GameObjectsCollection { get; set; }
		protected IList<IGameObject> GeneratedGameObjectsCollection { get; set; }
		protected ICommand CommandWhenUnloaded;
		protected bool PlayerWin;
		protected bool PlayerLoose;

		protected BaseScene() 
		{
			GameObjectsCollection = new List<IGameObject>();
			GeneratedGameObjectsCollection = new List<IGameObject>();
			PlayerWin = PlayerLoose = false;
		}

		public virtual void Win()
		{
			PlayerWin = false;
		}
		public virtual void Loose()
		{
			PlayerLoose = false;
		}

		public virtual void Load() { }

		public virtual void UnLoad(ICommand commandWhenUnloaded) 
		{
			CommandWhenUnloaded = commandWhenUnloaded;
			CommandWhenUnloaded.Execute();
		}

		public IGameObject GetObject(Func<IGameObject, bool> predicate)
		{
			return (GameObjectsCollection.FirstOrDefault(predicate));
		}

		public IList<IGameObject> GetObjects(Func<IGameObject, bool> predicate)
		{
			return (GameObjectsCollection.Where(predicate).ToList());
		}

		public virtual IGameObject RegisterGameObject(IGameObject gameObject)
		{
			GeneratedGameObjectsCollection.Add(gameObject);
			return (gameObject);
		}
		public virtual void RegisterGameObjects(IList<IGameObject> gameObjects)
		{
			(GameObjectsCollection as List<IGameObject>).AddRange(gameObjects);
		}

		public virtual IGameObject UnRegisterGameObject(IGameObject gameObject)
		{
			GameObjectsCollection.Remove(gameObject);
			return (gameObject);
		}

		public virtual void Update(GameTime gameTime)
		{
			// Gestion des collisions des GameObjects, et uniquement ceux qui sont actifs et qui dispose d'une collideBox
			CollisionTestResult collisionResult = null;
			IGameObject goi = null;
			IGameObject goj = null;
			for (int i = 0; i < GameObjectsCollection.Count; i++)
			{
				goi = GameObjectsCollection[i];
				goi.Update(gameTime);
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
			(GameObjectsCollection as List<IGameObject>).RemoveAll(gameObject => gameObject.Status == GameObjectStatus.OUTDATED);

			// On ajoute à la scène les éléments générés
			RegisterGameObjects(GeneratedGameObjectsCollection);
			GeneratedGameObjectsCollection.Clear();
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
