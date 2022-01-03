using System.Drawing.Printing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class MainGame : Game
	{
		private static readonly Point TargetResolution = new(1280, 800);

		private readonly GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private ISceneService GameState;
		private IInputListenerService InputListener;
		private RenderTarget2D RenderTarget;
		private Rectangle DrawTarget;
		private bool PreviousFullScreen;

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this)
			{
				GraphicsProfile = GraphicsProfile.HiDef
			};
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			Window.ClientSizeChanged += Window_ClientSizeChanged;
		}

		protected override void Initialize()
		{
			base.Initialize();

			// Changement de la résolution du jeu
			RenderTarget = new RenderTarget2D(_graphics.GraphicsDevice, TargetResolution.X, TargetResolution.Y);
			_graphics.PreferredBackBufferWidth = TargetResolution.X;
			_graphics.PreferredBackBufferHeight = TargetResolution.Y;
			_graphics.IsFullScreen = false;
			_graphics.ApplyChanges();
			PreviousFullScreen = !_graphics.IsFullScreen;


			// Enregistrement des services
			GameNameSpace.Services.Instance.Register<ILogService>(new LogService(new ConsoleLogger()));
			GameNameSpace.Services.Instance.Register<IScreenService>(new ScreenService(Window.ClientBounds));
			GameNameSpace.Services.Instance.Register<IShapeService>(new ShapeService(_spriteBatch));
			GameNameSpace.Services.Instance.Register<IRandomService>(new RandomService());
			GameNameSpace.Services.Instance.Register<IColliderService>(new ColliderService());
			GameNameSpace.Services.Instance.Register<ITweeningService>(new TweeningService());
			GameNameSpace.Services.Instance.Register<IParticlesService>(new ParticleService(2500));
			GameNameSpace.Services.Instance.Register<IGameObjectFactoryService>(new GameObjectFactoryService());
			GameNameSpace.Services.Instance.Register<ILevelService>(new JSONLevelService());
			InputListener = GameNameSpace.Services.Instance.Register<IInputListenerService>(new InputListenerService());
			GameState = GameNameSpace.Services.Instance.Register<ISceneService>(new SceneService(_graphics));

			// Redirection vers la 1ere scène du jeu
			GameState.ChangeScene(SceneType.MENU, new DummyCommand());
		}

		private void Window_ClientSizeChanged(object sender, System.EventArgs e)
		{
			if (_graphics.IsFullScreen)
			{
				_graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				_graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			}
			else
			{
				_graphics.PreferredBackBufferWidth = TargetResolution.X;
				_graphics.PreferredBackBufferHeight = TargetResolution.Y;
			}
			_graphics.ApplyChanges();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Chargement des ressources
			GameNameSpace.Services.Instance.Register<IAssetService>(new AssetService()).Load(Content, _spriteBatch.GraphicsDevice);
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
			GameNameSpace.Services.Instance.Get<IAssetService>().UnLoad();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GameState.ExitRequired)
			{
				Exit();
			}

			// Mise à jour du jeu
			GameState.Update(gameTime);
			InputListener.Update(gameTime);

			// On met à jour le cadre de rendu
			if (_graphics.IsFullScreen != PreviousFullScreen)
			{
				PreviousFullScreen = _graphics.IsFullScreen;

				// On prépare la rendertarget
				int currentWidth, currentHeight;
				if (_graphics.IsFullScreen)
				{
					currentWidth = _graphics.GraphicsDevice.DisplayMode.Width;
					currentHeight = _graphics.GraphicsDevice.DisplayMode.Height;
				}
				else
				{
					currentWidth = TargetResolution.X;
					currentHeight = TargetResolution.Y;
				}

				float windowAspectRatio = (float)currentWidth / currentHeight;
				float targetAspectRatio = (float)TargetResolution.X / TargetResolution.Y;

				float ratio = 1.0f;
				int marginTop = 0, marginLeft = 0;
				if (windowAspectRatio != targetAspectRatio)
				{
					if (windowAspectRatio > targetAspectRatio)
					{
						ratio = (float)currentHeight / TargetResolution.Y;
						marginLeft = (int)((currentWidth - TargetResolution.X * ratio) * 0.5f);
					}
					else
					{
						ratio = (float)currentWidth / TargetResolution.X;
						marginTop = (int)((currentHeight - TargetResolution.Y * ratio) * 0.5f);
					}
				}
				DrawTarget = new Rectangle(new Point(marginLeft, marginTop), (new Vector2(TargetResolution.X * ratio, TargetResolution.Y * ratio).ToPoint()));
				GameNameSpace.Services.Instance.Get<IInputListenerService>().Ratio = ratio;
			}
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			// On draw vers cette texture
			GraphicsDevice.SetRenderTarget(RenderTarget);

			// Nettoyage de la texture
			GraphicsDevice.Clear(Color.Black);

			// Draw de la frame dans la texture
			_spriteBatch.Begin();
			GameState.Draw(_spriteBatch);
			_spriteBatch.End();

			// Reset de la redirection des draw
			GraphicsDevice.SetRenderTarget(null);

			// On draw la frame à l'écran
			_spriteBatch.Begin();
			_spriteBatch.Draw(RenderTarget, new Rectangle(DrawTarget.Location + GameState.CamShake.Value, DrawTarget.Size), Color.White);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
