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
		}

		public override void Load(ICommand commandWhenLoaded)
		{
			RegisterGameObject(new BackgroundGameObject(Services.Instance.Get<IShapeService>().CreateTexture(Services.Instance.Get<IScreenService>().GetScreenSize(), Color.LightGray)));

			Point screen = Services.Instance.Get<IScreenService>().GetScreenSize();
			SpriteFont spriteFont = Services.Instance.Get<IAssetService>().GetFont(FontName.Title);
			Vector2 textSize = spriteFont.MeasureString(TITLE);

			// Titre de la scène
			RegisterGameObject(new TextGameObject(new Vector2((screen.X - textSize.X) / 2, (screen.Y - textSize.Y) / 4), textSize, spriteFont, TITLE, Color.Black));
			RegisterGameObject(new InScreenTransitionGameObject(new CompositeCommand(commandWhenLoaded, new ResetTransitionRequiredCommand())));

		}

		public override void UnLoad(ICommand commandWhenUnloaded)
		{
			RegisterGameObject(new OutScreenTransitionGameObject(new CompositeCommand(commandWhenUnloaded, new ResetTransitionRequiredCommand())));
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (Services.Instance.Get<IInputListenerService>().IsLeftClick())
			{
				ReturnToMenuCommand.Execute();
			}
		}
	}
}
