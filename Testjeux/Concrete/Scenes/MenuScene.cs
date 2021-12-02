using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameNameSpace
{
	public class MenuScene : BaseScene
	{
		private static readonly string TITLE = "Menu";

		public MenuScene() : base()
		{
			Rectangle screen = ServiceLocator.Instance.Get<Game>().Window.ClientBounds;
			SpriteFont spriteFontTitle = ServiceLocator.Instance.Get<AssetManager>().Title;
			Vector2 textSize = spriteFontTitle.MeasureString(TITLE);

			// Titre de la scène
			RegisterGameObject(new TextGameObject(new Vector2((screen.Width - textSize.X) / 2, (screen.Height - textSize.Y) / 4), textSize.ToPoint(), spriteFontTitle, TITLE, Color.Black));

			// Bouton pour démarrer le jeu
			Point buttonSize = new Point(400, 100);
			RegisterGameObject(new ButtonGameObject(new Vector2((screen.Width - buttonSize.X) / 2, 2 * (screen.Height - buttonSize.Y) / 3), buttonSize, Color.Coral, ServiceLocator.Instance.Get<AssetManager>().Button, "Jouer", Color.Black, new SwitchSceneCommand(new GotoSceneCommand(GameState.SceneType.GAMEPLAY))));
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (ServiceLocator.Instance.Get<InputListener>().IsKeyDown(Keys.Escape))
			{
				ServiceLocator.Instance.Get<Game>().Exit();
			}
		}
	}
}
