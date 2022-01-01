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
			ISceneService gameState = Services.Instance.Get<ISceneService>();
			gameState.Mode = SceneModeNames.None;
			gameState.Life = gameState.Level = 0;

			Music = Services.Instance.Get<IAssetService>().GetMusic(MusicName.SpaceDifficulties);
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

			IGameObjectFactoryService factory = Services.Instance.Get<IGameObjectFactoryService>();

			//Titre de la scène
			SpriteFont font = Services.Instance.Get<IAssetService>().GetFont(FontName.GiantTitle);
			Vector2 textSize = font.MeasureString("SPACE");
			Vector2 destination = new((screen.X - textSize.X) * 0.5f, 125);
			Vector2 origin = new(destination.X, -800.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.GiantTitle), "SPACE", Color.Silver), origin, destination));

			font = Services.Instance.Get<IAssetService>().GetFont(FontName.BigTitle);
			textSize = font.MeasureString("BREAKER");
			destination = new Vector2((screen.X - textSize.X) * 0.5f, 320);
			origin = new Vector2(destination.X, -800.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, font, "BREAKER", Color.Silver), origin, destination));

			font = Services.Instance.Get<IAssetService>().GetFont(FontName.Button);
			textSize = font.MeasureString("Raphael DUCHOSSOY (gamecodeur.fr)");
			destination = new Vector2((screen.X - textSize.X) * 0.5f, 120);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, font, "Raphael DUCHOSSOY (gamecodeur.fr)", Color.Silver), origin, destination));

			// Ajout du curseur de souris
			RegisterGameObject(new CursorGameObject());

			// Bouton pour démarrer le jeu
			destination = new Vector2(900, 550);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Jouer", Color.Black, new FollowSerieSceneCommand(SceneType.GAMEPLAY, 1)), origin, destination));

			destination = new Vector2(900, 650);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Sélection", Color.Black, new SwitchSceneCommand(SceneType.SELECTION)), origin, destination));

			destination = new Vector2(125, 650);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Quitter", Color.Black, new ExitGameCommand()), origin, destination));

			// Animation de fond
			RegisterGameObject(new SnakeHeadGameObject(new Vector2(1600, 32)));

			RegisterGameObject(new InScreenTransitionGameObject(new CompositeCommand(commandWhenLoaded, new ResetTransitionRequiredCommand())));

			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = 0.5f;
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
