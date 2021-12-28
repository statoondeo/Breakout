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
		public int MaxLife { get; protected set; }
		public int Life { get; set; }
		public int Level { get; set; }
		public SceneModeNames Mode { get; set; }

		public SceneService() 
		{
			ExitRequired = false;
			CamShake = new CamShake();
			Mode = SceneModeNames.None;
			Life = Level = 0;
			MaxLife = 3;
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
			CurrentScene = newScene switch
			{
				SceneType.MENU => new MenuScene(),
				SceneType.SELECTION => new SelectionScene(),
				SceneType.GAMEPLAY => new GameplayScene(levelNumber),
				_ => null,
			};
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
