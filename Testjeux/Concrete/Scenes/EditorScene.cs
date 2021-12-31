using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public sealed class EditorScene : BaseScene
	{

		private static readonly Point TilesBasePosition = new Point(55, 63);
		private static readonly Point TilesBaseSize = new Point(65 * 18, 65 * 8);
		private readonly int[,] Tiles;
		private readonly object[,] GameObjects;
		private IGameObject Cursor;
		private readonly Texture2D Texture;
		private IGameObject WaterMark;
		private bool CanSave;

		private int BackgroundIndex;
		private IGameObject Background;

		private int MusicIndex;
		private Song Music;

		public EditorScene() 
			: base() 
		{
			Tiles = new int[8, 18] 
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};
			GameObjects = new BaseGameObject[8, 18];

			Texture = Services.Instance.Get<IShapeService>().CreateTexture(new Point(64), Color.GreenYellow);
			WaterMark = null;

			BackgroundIndex = 0;
			Background = null;

			MusicIndex = 0;
			Music = null;

			CanSave = true;
		}

		public override void Load(ICommand commandWhenLoaded)
		{
			base.Load(commandWhenLoaded);

			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars01), new Vector2(-15, 0)));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars02), new Vector2(-7, 0)));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars03), new Vector2(-3, 0)));

			IGameObjectFactoryService factory = Services.Instance.Get<IGameObjectFactoryService>();

			// Ajout du curseur de souris
			Cursor = RegisterGameObject(new CursorGameObject());

			//Titre de la scène
			Vector2 destination = Vector2.Zero;
			Vector2 origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Title), "Création de niveau", Color.White), origin, destination));

			// Boutons
			destination = new Vector2(695.0f, 654.0f);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Musique", Color.Black, new GenericCommand(
				delegate
				{
					if (null != Music)
					{
						MediaPlayer.Stop();
					}
					MusicIndex++;
					MusicIndex %= 8;
					Music = CreateMusic(MusicIndex);
					if (null != Music)
					{
						MediaPlayer.IsRepeating = true;
						MediaPlayer.Volume = 0.5f;
						MediaPlayer.Play(Music);
					}

				})), origin, destination));

			destination = new Vector2(988.0f, 654.0f);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Fond", Color.Black, new GenericCommand(
				delegate
				{
					if (null != Background)
					{
						Background.Status = GameObjectStatus.OUTDATED;
					}
					BackgroundIndex++;
					BackgroundIndex %= 5;
					Background = CreateBackground(BackgroundIndex);
					if (null != Background)
					{
						RegisterGameObject(Background);
					}

				})), origin, destination));

			destination = new Vector2(988.0f, 727.0f);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Enreg.", Color.Black, new GenericCommand(
				delegate
				{
					if (CanSave) { 
						System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
						{
							Filter = "JSON Level|*.json",
							Title = "Enregistrer un niveau Space Breaker"
						};
						saveFileDialog.ShowDialog();

						if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName))
						{
							ParsedLevel level = new ParsedLevel();

							// Condition de victoire (aucune brique en vie)
							level.Triggers.Add(new ParsedTrigger() { Type = 1 });

							// Textes accompagnant les écrans de victoire et défaite
							level.Texts.Add(new ParsedText() { Type = 0, Text = string.Empty });
							level.Texts.Add(new ParsedText() { Type = 1, Text = string.Empty });

							// Fond étoilé
							level.Backgrounds.Add(new ParsedBackground() { Type = 0, Texture = "Stars01", Velocity = new ParsedVector2() { X = -3, Y = 0 } });
							level.Backgrounds.Add(new ParsedBackground() { Type = 0, Texture = "Stars02", Velocity = new ParsedVector2() { X = -7, Y = 0 } });
							level.Backgrounds.Add(new ParsedBackground() { Type = 0, Texture = "Stars03", Velocity = new ParsedVector2() { X = -15, Y = 0 } });

							// Background sélectionné
							if (BackgroundIndex != 0)
							{
								level.Backgrounds.Add(new ParsedBackground()
								{
									Type = 1,
									Texture = BackgroundIndex switch
									{
										1 => TextureName.Gas0.ToString(),
										2 => TextureName.Gas1.ToString(),
										3 => TextureName.Gas2.ToString(),
										4 => TextureName.Gas3.ToString(),
										_ => string.Empty
									},
									AngleSpeed = 0.01f
								});
							}

							// Music sélectionnée
							if (MusicIndex != 0)
							{
								level.Music = new ParsedMusic()
								{
									Type = MusicIndex switch
									{
										1 => 4,
										2 => 1,
										3 => 0,
										4 => 3,
										5 => 2,
										6 => 5,
										7 => 6,
										_ => 0
									}
								};
							}

							// Murs autours de l'écran
							level.Bricks.Add(new ParsedBrick() { Type = 0, Position = new ParsedVector2() { X = -50, Y = -50 }, Size = new ParsedVector2() { X = 1380, Y = 50 } });
							level.Bricks.Add(new ParsedBrick() { Type = 0, Position = new ParsedVector2() { X = -50, Y = 0 }, Size = new ParsedVector2() { X = 50, Y = 800 } });
							level.Bricks.Add(new ParsedBrick() { Type = 0, Position = new ParsedVector2() { X = 1280, Y = 0 }, Size = new ParsedVector2() { X = 50, Y = 800 } });

							// Briques
							for (int i = 0; i < 8; i++)
							{
								for (int j = 0; j < 18; j++)
								{
									if (Tiles[i, j] != 0)
									{
										level.Bricks.Add(new ParsedBrick()
										{
											Type = Tiles[i, j] switch
											{
												1 => 1,
												2 => 7,
												3 => 8,
												4 => 3,
												5 => 9,
												_ => 0
											},
											Position = new ParsedVector2()
											{
												X = TilesBasePosition.X + j * 65.0f,
												Y = TilesBasePosition.Y + i * 65.0f
											}
										});
									}
								}
							}

							// Enregistrement du fichier
							Services.Instance.Get<ILevelService>().Save(level, saveFileDialog.FileName);
							CanSave = false;
						}
					}
				})), origin, destination));

			destination = new Vector2(0.0f, 727.0f);
			origin = new Vector2(destination.X, -300.0f);
			RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Retour", Color.Black, new SwitchSceneCommand(SceneType.MENU)), origin, destination));

			// Ajout du curseur de souris
			RegisterGameObject(new CursorGameObject());

			RegisterGameObject(new InScreenTransitionGameObject(new CompositeCommand(commandWhenLoaded, new ResetTransitionRequiredCommand())));

			if (null != Music)
			{
				MediaPlayer.IsRepeating = true;
				MediaPlayer.Volume = 0.5f;
				MediaPlayer.Play(Music);
			}
		}

		public override void UnLoad(ICommand commandWhenUnloaded)
		{
			RegisterGameObject(new OutScreenTransitionGameObject(new CompositeCommand(commandWhenUnloaded, new ResetTransitionRequiredCommand())));
			if (null != Music)
			{
				MediaPlayer.Stop();
			}
		}

		private IGameObject CreateSprite(int index, Vector2 position)
		{
			IGameObject gameObject = index switch
			{
				0 => null,
				1 => new WobblerGameObject(position, 1.0f),
				2 => new AtomGameObject(position, 1.0f),
				3 => new BlobGameObject(position, 1.0f),
				4 => new CubeGameObject(position),
				5 => new BonusTokenGameObject(position),
				_ => null
			};
			if (null != gameObject)
			{
				gameObject.Body = new InvisibleBody(gameObject.Body.Position);
			}
			return (gameObject);
		}

		private IGameObject CreateBackground(int index)
		{
			IGameObject gameObject = index switch
			{
				0 => null,
				1 => new RotatingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Gas0), 0.01f),
				2 => new RotatingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Gas1), 0.01f),
				3 => new RotatingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Gas2), 0.01f),
				4 => new RotatingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Gas3), 0.01f),
				_ => null
			};
			return (gameObject);
		}

		private Song CreateMusic(int index)
		{
			Song song = index switch
			{
				0 => null,
				1 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.BusyBeat),
				2 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.SpaceDifficulties),
				3 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.SpaceUtopia),
				4 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.SubterraneanMonster),
				5 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.SwampChase),
				6 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.TheThroneRoom),
				7 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.ZombieMarch),
				_ => null
			};
			return (song);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (Services.Instance.Get<IInputListenerService>().IsKeyDown(Keys.Escape))
			{
				(new SwitchSceneCommand(SceneType.SELECTION)).Execute();
			}

			if (Services.Instance.Get<IInputListenerService>().IsLeftClick())
			{
				CanSave = true;
			}

			// Affichage de la watermark
			if (null != WaterMark)
			{
				WaterMark.Status = GameObjectStatus.OUTDATED;
			}
			if ((new Rectangle(TilesBasePosition, TilesBaseSize)).Intersects(new Rectangle(Cursor.Body.Position.ToPoint(), new Point(1))))
			{
				Point tile = Cursor.Body.Position.ToPoint() - TilesBasePosition;
				tile = new Point((int)(tile.X / 65), (int)(tile.Y / 65));
				WaterMark = RegisterGameObject(new TextureGameObject(Texture));
				WaterMark.Renderable.Layer = 0.4f;
				WaterMark.Renderable.Alpha = 0.5f;
				WaterMark.Body.MoveTo(new Vector2(TilesBasePosition.X + tile.X * 65.0f, TilesBasePosition.Y + tile.Y * 65.0f));

				if (Services.Instance.Get<IInputListenerService>().IsLeftClick())
				{
					Tiles[tile.Y, tile.X]++;
					Tiles[tile.Y, tile.X] %= 6;
					if (GameObjects[tile.Y, tile.X] != null) 
					{
						UnRegisterGameObject(GameObjects[tile.Y, tile.X] as IGameObject);
					}
					GameObjects[tile.Y, tile.X] = CreateSprite(Tiles[tile.Y, tile.X], new Vector2(TilesBasePosition.X + tile.X * 65.0f, TilesBasePosition.Y + tile.Y * 65.0f));
					if (null != GameObjects[tile.Y, tile.X])
					{
						RegisterGameObject(GameObjects[tile.Y, tile.X] as IGameObject);
					}
				}
			}
		}
	}
}
