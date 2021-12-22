using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class MenuScene : BaseScene
	{
		public MenuScene() : base() { }

		public override void Load(ICommand commandWhenLoaded)
		{
			Point screen = Services.Instance.Get<IScreenService>().GetScreenSize();

			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars01), new Vector2(-15, 0), Vector2.Zero));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars01), new Vector2(-15, 0), new Vector2(screen.X, 0)));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars02), new Vector2(-7, 0), Vector2.Zero));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars02), new Vector2(-7, 0), new Vector2(screen.X, 0)));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars03), new Vector2(-3, 0), Vector2.Zero));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars03), new Vector2(-3, 0), new Vector2(screen.X, 0)));
			RegisterGameObject(new RotatingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Gas3), 0.01f));
			RegisterGameObject(new BigPanelGameObject());

			//Titre de la scène
			RegisterGameObject(new TextGameObject(new Vector2(95, 730), Services.Instance.Get<IAssetService>().GetFont(FontName.BigTitle), "SPACE", Color.White, -(float)Math.PI / 2));
			RegisterGameObject(new TextGameObject(new Vector2(260, 60), Services.Instance.Get<IAssetService>().GetFont(FontName.GiantTitle), "BREA", Color.White));
			RegisterGameObject(new TextGameObject(new Vector2(260, 250), Services.Instance.Get<IAssetService>().GetFont(FontName.BigTitle), "KER", Color.White));
			RegisterGameObject(new TextGameObject(new Vector2(260, 70), Services.Instance.Get<IAssetService>().GetFont(FontName.Button), "Raphael DUCHOSSOY (gamecodeur.fr)", Color.White));


			// Bouton pour démarrer le jeu
			RegisterGameObject(new ButtonGameObject(new Vector2(900, 450), "Jouer", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY, 1)));
			RegisterGameObject(new ButtonGameObject(new Vector2(900, 550), "Sélection", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY, 2)));
			RegisterGameObject(new ButtonGameObject(new Vector2(900, 650), "Quitter", Color.Black, new ExitGameCommand()));

			// Ajout du curseur de souris
			RegisterGameObject(new CursorGameObject());

			RegisterGameObject(new InScreenTransitionGameObject(new CompositeCommand(commandWhenLoaded, new ResetTransitionRequiredCommand())));
		}

		public override void UnLoad(ICommand commandWhenUnloaded)
		{
			RegisterGameObject(new OutScreenTransitionGameObject(new CompositeCommand(commandWhenUnloaded, new ResetTransitionRequiredCommand())));
		}

		public override void Update(GameTime gameTime)
		{
			//if (ServiceLocator.Instance.Get<IInputListenerService>().IsLeftClick())
			//{
			//	(new BallExplosionParticlesEmitter(Cursor, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 15)).Emit();
			//}

			base.Update(gameTime);
		}
	}
}
