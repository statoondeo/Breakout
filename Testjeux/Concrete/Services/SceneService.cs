using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class SceneService : ISceneService
	{
		protected IScene CurrentScene;

		public SceneService() { }

		public void Load()
		{
			CurrentScene.Load();
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
		}

		public void Draw(SpriteBatch spriteBatch)
{
			CurrentScene.Draw(spriteBatch);
		}

		public IScene ChangeScene(SceneType newScene)
		{
			return (ChangeScene(newScene, 0));
		}

		public IScene ChangeScene(SceneType newScene, int levelNumber)
		{
			CurrentScene = newScene switch
			{
				SceneType.MENU => new MenuScene(),
				SceneType.GAMEPLAY => new GameplayScene(levelNumber),
				SceneType.GAMEOVER => new GameOverScene(),
				SceneType.VICTORY => new VictoryScene(),
				_ => null,
			};
			CurrentScene.Load();
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
	}
}
