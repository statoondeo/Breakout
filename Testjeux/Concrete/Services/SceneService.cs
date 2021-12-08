using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class SceneService : ISceneService
	{
		protected IScene CurrentScene;

		public SceneService() { }

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

		public IScene GetCurrent()
		{
			return (CurrentScene);
		}
	}
}
