using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class VictoryScene : BaseScene
	{
		private static readonly string TITLE = "Victoire";

		protected ICommand ReturnToMenuCommand;

		public VictoryScene() : base()
		{
			ReturnToMenuCommand = new SwitchSceneCommand(SceneType.MENU);

			Rectangle screen = ServiceLocator.Instance.Get<Game>().Window.ClientBounds;
			SpriteFont spriteFont = ServiceLocator.Instance.Get<AssetManager>().Title;
			Vector2 textSize = spriteFont.MeasureString(TITLE);

			// Titre de la scène
			RegisterGameObject(new TextGameObject(new Vector2((screen.Width - textSize.X) / 2, (screen.Height - textSize.Y) / 4), textSize.ToPoint(), spriteFont, TITLE, Color.Black));
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
