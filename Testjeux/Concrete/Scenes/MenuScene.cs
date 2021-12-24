using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public sealed class MenuScene : BaseScene
	{
		private readonly Song Music;

		public MenuScene() 
			: base() 
		{
			Music = Services.Instance.Get<IAssetService>().GetMusic(MusicName.SpaceUtopia);
		}

		public override void Load(ICommand commandWhenLoaded)
		{
			base.Load(commandWhenLoaded);
			Point screen = Services.Instance.Get<IScreenService>().GetScreenSize();

			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars01), new Vector2(-15, 0)));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars02), new Vector2(-7, 0)));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars03), new Vector2(-3, 0)));
			RegisterGameObject(new RotatingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Gas3), 0.01f));
			RegisterGameObject(new BigPanelGameObject());

			//Titre de la scène
			SpriteFont font = Services.Instance.Get<IAssetService>().GetFont(FontName.GiantTitle);
			Vector2 textSize = font.MeasureString("SPACE");
			RegisterGameObject(new TextGameObject(new Vector2((screen.X - textSize.X) * 0.5f, 125), Services.Instance.Get<IAssetService>().GetFont(FontName.GiantTitle), "SPACE", Color.Silver));

			font = Services.Instance.Get<IAssetService>().GetFont(FontName.BigTitle);
			textSize = font.MeasureString("BREAKER");
			RegisterGameObject(new TextGameObject(new Vector2((screen.X - textSize.X) * 0.5f, 320), font, "BREAKER", Color.Silver));

			font = Services.Instance.Get<IAssetService>().GetFont(FontName.Button);
			textSize = font.MeasureString("Raphael DUCHOSSOY (gamecodeur.fr)");
			RegisterGameObject(new TextGameObject(new Vector2((screen.X - textSize.X) * 0.5f, 120), font, "Raphael DUCHOSSOY (gamecodeur.fr)", Color.Silver));


			// Bouton pour démarrer le jeu
			RegisterGameObject(new ButtonGameObject(new Vector2(900, 550), "Jouer", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY, 1)));
			RegisterGameObject(new ButtonGameObject(new Vector2(900, 650), "Sélection", Color.Black, new SwitchSceneCommand(SceneType.SELECTION)));
			RegisterGameObject(new ButtonGameObject(new Vector2(125, 650), "Quitter", Color.Black, new ExitGameCommand()));

			// Ajout du curseur de souris
			RegisterGameObject(new CursorGameObject());

			// Animation de fond
			RegisterGameObject(new SnakeHeadGameObject(new Vector2(1600, 32)));

			RegisterGameObject(new InScreenTransitionGameObject(new CompositeCommand(commandWhenLoaded, new ResetTransitionRequiredCommand())));

			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(Music);
		}

		public override void UnLoad(ICommand commandWhenUnloaded)
		{
			RegisterGameObject(new OutScreenTransitionGameObject(new CompositeCommand(commandWhenUnloaded, new ResetTransitionRequiredCommand())));
			MediaPlayer.Stop();
		}

		public override void Update(GameTime gameTime)
		{
			if (Services.Instance.Get<IInputListenerService>().IsLeftClick())
			{
				IGameObject cursor = GetObject(item => item is CursorGameObject);
				(new BallExplosionParticlesEmitter(cursor, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 25)).Emit();
			}

			base.Update(gameTime);
		}
	}
}
