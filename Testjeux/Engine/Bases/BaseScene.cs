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
		public bool PlayerWin { get; protected set; }
		public bool PlayerLoose { get; protected set; }

		protected BaseScene() 
		{
			GameObjectsCollection = new List<IGameObject>();
			GeneratedGameObjectsCollection = new List<IGameObject>();
			PlayerWin = PlayerLoose = false;
			ResetTransition();
		}

		public virtual void Win()
		{
			PlayerWin = true;
		}
		public virtual void Loose()
		{
			PlayerLoose = true;
		}

		public virtual void Load(ICommand commandWhenLoaded) 
		{
			Services.Instance.Get<IParticlesService>().Reset();
		}

		public virtual void UnLoad(ICommand commandWhenUnloaded) 
		{
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
			GameObjectsCollection.Add(gameObject);
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
			IColliderService collider = Services.Instance.Get<IColliderService>();
			CollisionTestResult collisionResult = null;
			IGameObject goi = null;
			IGameObject goj = null;
			for (int i = 0; i < GameObjectsCollection.Count; i++)
			{
				goi = GameObjectsCollection[i];
				if (goi.Status == GameObjectStatus.ACTIVE)
				{
					goi.Update(gameTime);
					if (!(goi.Body is InvisibleBody))
					{
						for (int j = i + 1; j < GameObjectsCollection.Count; j++)
						{
							goj = GameObjectsCollection[j];

							if (goj.Status == GameObjectStatus.ACTIVE
								&& !(goj.Body is InvisibleBody)
								&& (goi.Type != goj.Type)
								&& (null != (collisionResult = collider.IsCollision(goi.Body, goj.Body))))
							{
								if (!(goi is IButtonGameObject) && !(goj is IButtonGameObject))
								{
									collider.ResolveCollision(goi.Body, goj.Body, collisionResult);
								}
								goi.Body.CollideCommand.Execute(goj, collisionResult);
								goj.Body.CollideCommand.Execute(goi, collisionResult);
							}
						}
					}
				}
			}

			// On purge tous les éléments obsolètes
			(GameObjectsCollection as List<IGameObject>).RemoveAll(gameObject => gameObject.Status == GameObjectStatus.OUTDATED);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			// On n'affiche que les éléments actifs
			foreach (IGameObject gameObject in GameObjectsCollection.Where(gameObject => gameObject.Status == GameObjectStatus.ACTIVE).OrderBy(item => item.Renderable.Layer))
			{
				gameObject.Draw(spriteBatch);
			}
		}

		public void ResetTransition()
		{
		}
	}
}
