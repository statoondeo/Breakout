using System;
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
			switch (newScene)
			{
				case SceneType.MENU:
					CurrentScene = new MenuScene();
					break;
				case SceneType.GAMEPLAY:
					CurrentScene = new GameplayScene();
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
			CurrentScene.Load();
			return (CurrentScene);
		}
	}
}
