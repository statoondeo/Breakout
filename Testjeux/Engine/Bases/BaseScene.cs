using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameNameSpace
{
	public abstract class BaseScene : IScene
	{
		protected IList<IGameObject> GameObjectsCollection { get; set; }
		protected IList<IGameObject> BricksCollection { get; set; }
		protected IList<IGameObject> BallsCollection { get; set; }
		protected IList<IGameObject> ButtonsCollection { get; set; }

		public bool PlayerWin { get; protected set; }
		public bool PlayerLoose { get; protected set; }

		protected BaseScene()
		{
			GameObjectsCollection = new List<IGameObject>();
			BricksCollection = new List<IGameObject>();
			BallsCollection = new List<IGameObject>();
			ButtonsCollection = new List<IGameObject>();
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
			if (gameObject.Type == GameObjectType.BALL)
			{
				BallsCollection.Add(gameObject);
			}
			else if (gameObject.Type == GameObjectType.BRICK || gameObject.Type == GameObjectType.RACKET || gameObject.Type == GameObjectType.WALL)
			{
				BricksCollection.Add(gameObject);
			}
			else if (gameObject.Type == GameObjectType.BUTTON)
			{
				ButtonsCollection.Add(gameObject);
			}
			return (gameObject);
		}

		public virtual void RegisterGameObjects(IList<IGameObject> gameObjects)
		{
			for (int i = 0; i < gameObjects.Count; i++)
			{
				RegisterGameObject(gameObjects[i]);
			}
		}

		public virtual IGameObject UnRegisterGameObject(IGameObject gameObject)
		{
			GameObjectsCollection.Remove(gameObject);
			if (gameObject.Type == GameObjectType.BALL)
			{
				BallsCollection.Remove(gameObject);
			}
			else if (gameObject.Type == GameObjectType.BRICK || gameObject.Type == GameObjectType.RACKET || gameObject.Type == GameObjectType.WALL)
			{
				BricksCollection.Remove(gameObject);
			}
			else if (gameObject.Type == GameObjectType.BUTTON)
			{
				ButtonsCollection.Remove(gameObject);
			}
			return (gameObject);
		}

		public virtual void Update(GameTime gameTime)
		{
			if (Services.Instance.Get<IInputListenerService>().IsKeyPressed(Keys.F1))
			{
				Services.Instance.Get<ISceneService>().ToggleFullScreen();
			}

			IColliderService collider = Services.Instance.Get<IColliderService>();
			CollisionTestResult collisionResult = null;
			
			// Gestion des update
			for (int i = 0; i < GameObjectsCollection.Count; i++)
			{
				IGameObject gameObject = GameObjectsCollection[i];
				if (gameObject.Status == GameObjectStatus.ACTIVE)
				{
					gameObject.Update(gameTime);
				}
			}

			// Gestions des collisions (contre balles et briques, raquettes et murs)
			for (int i = 0; i < BallsCollection.Count; i++)
			{
				IGameObject ballGameobject = BallsCollection[i];
				for (int j = 0; j < BricksCollection.Count; j++)
				{
					IGameObject brickGameObject = BricksCollection[j];
					if ((brickGameObject.Status == GameObjectStatus.ACTIVE)
						&& (null != (collisionResult = collider.IsCollision(ballGameobject.Body, brickGameObject.Body))))
					{

						collider.ResolveCollision(ballGameobject.Body, brickGameObject.Body, collisionResult);
						ballGameobject.Body.CollideCommand.Execute(brickGameObject, collisionResult);
						brickGameObject.Body.CollideCommand.Execute(ballGameobject, collisionResult);
					}
				}
			}

			// Gestion du curseur
			IGameObject cursor = GameObjectsCollection.FirstOrDefault(item => item is CursorGameObject);
			if (cursor != null)
			{
				for (int i = 0; i < ButtonsCollection.Count; i++)
				{
					IGameObject buttonGameObject = ButtonsCollection[i];
					if ((buttonGameObject.Status == GameObjectStatus.ACTIVE)
						&& (null != (collisionResult = collider.IsCollision(cursor.Body, buttonGameObject.Body))))
					{
						buttonGameObject.Body.CollideCommand.Execute(cursor, collisionResult);
					}
				}
			}

			//for (int i = 0; i < GameObjectsCollection.Count; i++)
			//{
			//	goi = GameObjectsCollection[i];
			//	if (goi.Status == GameObjectStatus.ACTIVE)
			//	{
			//		goi.Update(gameTime);
			//		if (!(goi.Body is InvisibleBody))
			//		{
			//			for (int j = i + 1; j < GameObjectsCollection.Count; j++)
			//			{
			//				goj = GameObjectsCollection[j];

			//				if (goj.Status == GameObjectStatus.ACTIVE
			//					&& !(goj.Body is InvisibleBody)
			//					&& (goi.Type != goj.Type)
			//					&& (null != (collisionResult = collider.IsCollision(goi.Body, goj.Body))))
			//				{
			//					if (!(goi is IButtonGameObject) && !(goj is IButtonGameObject))
			//					{
			//						collider.ResolveCollision(goi.Body, goj.Body, collisionResult);
			//					}
			//					goi.Body.CollideCommand.Execute(goj, collisionResult);
			//					goj.Body.CollideCommand.Execute(goi, collisionResult);
			//				}
			//			}
			//		}
			//	}
			//}

			// On purge tous les éléments obsolètes
			(GameObjectsCollection as List<IGameObject>).RemoveAll(gameObject => gameObject.Status == GameObjectStatus.OUTDATED);
			(BricksCollection as List<IGameObject>).RemoveAll(gameObject => gameObject.Status == GameObjectStatus.OUTDATED);
			(BallsCollection as List<IGameObject>).RemoveAll(gameObject => gameObject.Status == GameObjectStatus.OUTDATED);
			(ButtonsCollection as List<IGameObject>).RemoveAll(gameObject => gameObject.Status == GameObjectStatus.OUTDATED);
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
