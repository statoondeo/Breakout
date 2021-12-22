using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class SceneService : ISceneService
	{
		public bool ExitRequired { get; set; }
		public CamShake CamShake { get; protected set; }

		public IScene CurrentScene { get; protected set; }

		public SceneService() 
		{
			ExitRequired = false;
			CamShake = new CamShake();
		}

		public void Load(ICommand commandWhenLoaded)
		{
			CurrentScene.Load(commandWhenLoaded);
		}

		public void UnLoad(ICommand command)
		{
			CurrentScene.UnLoad(command);
		}

		public IGameObject RegisterGameObject(IGameObject gameObject)
		{
			return (CurrentScene.RegisterGameObject(gameObject));
		}

		public IGameObject UnRegisterGameObject(IGameObject gameObject)
		{
			return (CurrentScene.UnRegisterGameObject(gameObject));
		}

		public void Update(GameTime gameTime)
		{
			CurrentScene.Update(gameTime);
			CamShake.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
{
			CurrentScene.Draw(spriteBatch);
		}

		public IScene ChangeScene(SceneType newScene, ICommand whenLoadedCommand)
		{
			return (ChangeScene(newScene, 0, whenLoadedCommand));
		}

		public IScene ChangeScene(SceneType newScene, int levelNumber, ICommand whenLoadedCommand)
		{
			if ((newScene == SceneType.GAMEPLAY) && (levelNumber > Services.Instance.Get<ILevelService>().MaxLevel))
			{
				newScene = SceneType.VICTORY;
			}
			switch(newScene)
			{
				case SceneType.MENU:
					CurrentScene = new MenuScene();
					break;
				case SceneType.GAMEPLAY:
					if (levelNumber > Services.Instance.Get<ILevelService>().MaxLevel)
					{
						CurrentScene = new VictoryScene();
					}
					else
					{
						CurrentScene = new GameplayScene(levelNumber);
					}
					break;
				case SceneType.GAMEOVER:
					CurrentScene = new GameOverScene();
					break;
				case SceneType.VICTORY:
					CurrentScene = new VictoryScene();
					break;
				default:
					CurrentScene = null;
					break;
			}
			CurrentScene?.Load(whenLoadedCommand);
			return (CurrentScene);
		}

		public IGameObject GetObject(Func<IGameObject, bool> predicate)
		{
			return (CurrentScene.GetObject(predicate));
		}

		public IList<IGameObject> GetObjects(Func<IGameObject, bool> predicate)
		{
			return (CurrentScene.GetObjects(predicate));
		}

		public void Win()
		{
			CurrentScene.Win();
		}

		public void Loose()
		{
			CurrentScene.Loose();
		}

		public void ResetTransition()
		{
			CurrentScene.ResetTransition();
		}

		public void RegisterGameObjects(IList<IGameObject> gameObjects)
		{
			CurrentScene.RegisterGameObjects(gameObjects);
		}
	}
}
