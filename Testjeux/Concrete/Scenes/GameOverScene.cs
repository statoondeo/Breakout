using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class GameOverScene : BaseScene
	{
		private static readonly string TITLE = "Défaite";

		protected ICommand ReturnToMenuCommand;

		public GameOverScene() : base()
		{
			ReturnToMenuCommand = new SwitchSceneCommand(SceneType.MENU);

			Rectangle screen = ServiceLocator.Instance.Get<Game>().Window.ClientBounds;
			SpriteFont spriteFont = ServiceLocator.Instance.Get<AssetManager>().Title;
			Vector2 textSize = spriteFont.MeasureString(TITLE);

			// Titre de la scène
			RegisterGameObject(new TextGameObject(new Vector2((screen.Width - textSize.X) / 2, (screen.Height - textSize.Y) / 4), textSize, spriteFont, TITLE, Color.Black));
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (ServiceLocator.Instance.Get<InputListener>().IsLeftClick())
			{
				ReturnToMenuCommand.Execute();
			}
		}
	}
}
