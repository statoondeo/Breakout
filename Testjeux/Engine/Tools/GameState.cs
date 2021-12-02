using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class GameState
	{
		public enum SceneType
		{
			MENU,
			GAMEPLAY,
			GAMEOVER,
			VICTORY
		}

		public IScene CurrentScene { get; protected set; }

		public GameState()
		{
		}

		public void Update(GameTime gameTime)
		{
			CurrentScene.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
{
			CurrentScene.Draw(spriteBatch);
		}

		public void ChangeScene(SceneType newScene)
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
		}
	}
}
