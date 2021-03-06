using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
			Vector2 destination = new(125, 75);
			Vector2 origin = new(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Title), "Sélection du niveau", Color.White), origin, destination));

			// Présentation des niveaux
			destination = Vector2.Zero;
			origin = new Vector2(destination.X, -8300.0f);
			RegisterGameObject(factory.DecorateEntrance(new BigPanelGameObject(), origin, destination));

			destination = new Vector2(70, 220);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new CardPanelGameObject(origin), origin, destination));

			destination = new Vector2(495, 220);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new CardPanelGameObject(origin), origin, destination));

			destination = new Vector2(920, 220);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new CardPanelGameObject(origin), origin, destination));

			// On ajoute les sprites des boss
			destination = new Vector2(117, 320);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new BrainMiniatureGameObject(origin), origin, destination));

			destination = new Vector2(565, 330);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new BlobMiniatureGameObject(origin), origin, destination));

			destination = new Vector2(963, 303);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SnakeMiniatureGameObject(origin), origin, destination));

			// Textes
			destination = new Vector2(122, 260);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "Monde 1", Color.White), origin, destination));

			destination = new Vector2(540, 260);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "Monde 2", Color.White), origin, destination));

			destination = new Vector2(960, 260);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "Monde 3", Color.White), origin, destination));

			destination = new Vector2(139, 450);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "BRAIN", Color.White), origin, destination));

			destination = new Vector2(570, 450);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "BLOB", Color.White), origin, destination));

			destination = new Vector2(980, 450);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "SNAKE", Color.White), origin, destination));

			// Boutons
			destination = new Vector2(320, 250);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "1", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 1)), origin, destination));

			destination = new Vector2(320, 350);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "2", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 2)), origin, destination));

			destination = new Vector2(320, 450);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "3", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 3)), origin, destination));

			destination = new Vector2(320, 550);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "4", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 4)), origin, destination));

			destination = new Vector2(20, 550);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Boss", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 5)), origin, destination));

			destination = new Vector2(745, 250);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "1", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 6)), origin, destination));

			destination = new Vector2(745, 350);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "2", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 7)), origin, destination));

			destination = new Vector2(745, 450);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "3", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 8)), origin, destination));

			destination = new Vector2(745, 550);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "4", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 9)), origin, destination));

			destination = new Vector2(445, 550);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Boss", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 10)), origin, destination));

			destination = new Vector2(1170, 250);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "1", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 11)), origin, destination));

			destination = new Vector2(1170, 350);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "2", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 12)), origin, destination));

			destination = new Vector2(1170, 450);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "3", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 13)), origin, destination));

			destination = new Vector2(1170, 550);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new SmallButtonGameObject(origin, "4", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 14)), origin, destination));

			destination = new Vector2(870, 550);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Boss", Color.Black, new FollowSelectionSceneCommand(SceneType.GAMEPLAY, 15)), origin, destination));

			destination = new Vector2(490, 650);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Charger", Color.Black, new GenericCommand(delegate
			{
				System.Windows.Forms.OpenFileDialog openFileDialog = new()
				{
					Filter = "JSON Level|*.json",
					Title = "Enregistrer un niveau Space Breaker"
				};
				openFileDialog.ShowDialog();
				if (!string.IsNullOrWhiteSpace(openFileDialog.FileName))
				{
					ParsedLevel level;
					try
					{
						level = Services.Instance.Get<ILevelService>().Load(openFileDialog.FileName);
						Services.Instance.Get<IGameObjectFactoryService>().CreateLevel(level);
						(new SwitchSceneCommand(SceneType.GAMEPLAY, level)).Execute();
					}
					catch (Exception)
					{
						Point screen = Services.Instance.Get<IScreenService>().GetScreenSize();
						string label = "Erreur : Format de fichier incorrect";
						SpriteFont font = Services.Instance.Get<IAssetService>().GetFont(FontName.Button);
						Vector2 textSize = font.MeasureString(label);
						destination = new Vector2((screen.X - textSize.X) * 0.5f, (screen.Y - textSize.Y) * 0.5f);
						RegisterGameObject(new ErrorTextGameObject(destination, font, label, Color.Red));
					}
				}
			})), origin, destination));

			destination = new Vector2(855, 650);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Créer", Color.Black, new SwitchSceneCommand(SceneType.EDITOR)), origin, destination));

			destination = new Vector2(125, 650);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Retour", Color.Black, new SwitchSceneCommand(SceneType.MENU)), origin, destination));

			// Ajout du curseur de souris
			RegisterGameObject(new CursorGameObject());

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
			base.Update(gameTime);
			if (Services.Instance.Get<IInputListenerService>().IsKeyDown(Keys.Escape))
			{
				(new SwitchSceneCommand(SceneType.MENU)).Execute();
			}
		}
	}
}
