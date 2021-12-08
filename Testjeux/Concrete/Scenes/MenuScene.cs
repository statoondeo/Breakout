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
			Point screen = ServiceLocator.Instance.Get<IScreenService>().GetScreenSize();
			SpriteFont spriteFontTitle = ServiceLocator.Instance.Get<IAssetService>().GetFont(FontName.Title);
			Vector2 textSize = spriteFontTitle.MeasureString(TITLE);

			//Titre de la scène
			RegisterGameObject(new TextGameObject(new Vector2((screen.X - textSize.X) / 2, (screen.Y - textSize.Y) / 4), textSize, spriteFontTitle, TITLE, Color.Black));

			// Bouton pour démarrer le jeu
			Vector2 buttonSize = new Vector2(400, 100);
			RegisterGameObject(new ButtonGameObject(new Vector2((screen.X - buttonSize.X) / 2, 2 * (screen.Y - buttonSize.Y) / 3), buttonSize, Color.Coral, ServiceLocator.Instance.Get<IAssetService>().GetFont(FontName.Button), "Jouer", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY)));

			// AJout du curseur de souris
			RegisterGameObject(new CursorGameObject());
		}

		public override void Update(GameTime gameTime)
		{
			if (ServiceLocator.Instance.Get<IInputListenerService>().IsKeyDown(Keys.Escape))
			{
				// TODO
				ServiceLocator.Instance.Get<Game>().Exit();
			}

			base.Update(gameTime);
		}
	}
}
