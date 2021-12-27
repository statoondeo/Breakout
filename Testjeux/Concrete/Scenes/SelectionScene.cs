using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public sealed class SelectionScene : BaseScene
	{
		private readonly Song Music;

		public SelectionScene() 
			: base() 
		{
			Music = Services.Instance.Get<IAssetService>().GetMusic(MusicName.SpaceUtopia);
		}

		public override void Load(ICommand commandWhenLoaded)
		{
			base.Load(commandWhenLoaded);

			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars01), new Vector2(-15, 0)));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars02), new Vector2(-7, 0)));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars03), new Vector2(-3, 0)));
			RegisterGameObject(new RotatingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Gas3), 0.01f));

			IGameObjectFactoryService factory = Services.Instance.Get<IGameObjectFactoryService>();

			//Titre de la scène
			Vector2 destination = new Vector2(125, 75);
			Vector2 origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Title), "Sélection du niveau", Color.White), origin, destination));

			// Présentation des niveaux
			destination = Vector2.Zero;
			origin = new Vector2(destination.X, -8300.0f);
			RegisterGameObject(factory.DecorateEntrance(new BigPanelGameObject(), origin, destination));

			destination = new Vector2(168, 220);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new CardPanelGameObject(origin), origin, destination));

			destination = new Vector2(533, 220);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new CardPanelGameObject(origin), origin, destination));

			destination = new Vector2(898, 220);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new CardPanelGameObject(origin), origin, destination));

			// On ajoute les sprites des boss
			destination = new Vector2(215, 320);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new BrainMiniatureGameObject(origin), origin, destination));

			destination = new Vector2(600, 330);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new BlobMiniatureGameObject(origin), origin, destination));

			destination = new Vector2(943, 303);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SnakeMiniatureGameObject(origin), origin, destination));

			// Textes
			destination = new Vector2(220, 260);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "Niveau 1", Color.White), origin, destination));

			destination = new Vector2(575, 260);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "Niveau 2", Color.White), origin, destination));

			destination = new Vector2(940, 260);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "Niveau 3", Color.White), origin, destination));

			destination = new Vector2(237, 450);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "BRAIN", Color.White), origin, destination));

			destination = new Vector2(605, 450);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "BLOB", Color.White), origin, destination));

			destination = new Vector2(960, 450);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "SNAKE", Color.White), origin, destination));

			// Boutons
			destination = new Vector2(125, 550);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Jouer", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY, 1)), origin, destination));

			destination = new Vector2(490, 550);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Jouer", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY, 2)), origin, destination));

			destination = new Vector2(855, 550);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Jouer", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY, 3)), origin, destination));

			destination = new Vector2(125, 650);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Retour", Color.Black, new SwitchSceneCommand(SceneType.MENU)), origin, destination));

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
	}
}
