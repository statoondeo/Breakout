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

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this)
			{
				GraphicsProfile = GraphicsProfile.HiDef
			};
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();

			// On prépare la rendertarget
			RenderTarget = new RenderTarget2D(_graphics.GraphicsDevice, TargetResolution.X, TargetResolution.Y);

			// Changement de la résolution du jeu
			_graphics.PreferredBackBufferWidth = TargetResolution.X;
			_graphics.PreferredBackBufferHeight = TargetResolution.Y;
			//_graphics.IsFullScreen = true;
			_graphics.ApplyChanges();

			// Enregistrement des services
			GameNameSpace.Services.Instance.Register<ILogService>(new LogService(new ConsoleLogger()));
			GameNameSpace.Services.Instance.Register<IScreenService>(new ScreenService(Window.ClientBounds));
			GameNameSpace.Services.Instance.Register<IShapeService>(new ShapeService(_spriteBatch));
			GameNameSpace.Services.Instance.Register<IRandomService>(new RandomService());
			GameNameSpace.Services.Instance.Register<IColliderService>(new ColliderService());
			GameNameSpace.Services.Instance.Register<ITweeningService>(new TweeningService());
			GameNameSpace.Services.Instance.Register<IParticlesService>(new ParticleService(1500));
			GameNameSpace.Services.Instance.Register<IGameObjectFactoryService>(new GameObjectFactoryService());
			GameNameSpace.Services.Instance.Register<ILevelService>(new JSONLevelService());
			InputListener = GameNameSpace.Services.Instance.Register<IInputListenerService>(new InputListenerService());
			GameState = GameNameSpace.Services.Instance.Register<ISceneService>(new SceneService());

			// Redirection vers la 1ere scène du jeu
			GameState.ChangeScene(SceneType.MENU, new DummyCommand());
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
			float windowAspectRatio = (float)Window.ClientBounds.Width / Window.ClientBounds.Height;
			float targetAspectRatio = (float)TargetResolution.X / TargetResolution.Y;
			float ratio = 1.0f;
			int marginTop = 0, marginLeft = 0;
			if (windowAspectRatio != targetAspectRatio)
			{
				if (windowAspectRatio > targetAspectRatio)
				{
					ratio = (float)Window.ClientBounds.Height / TargetResolution.Y;
					marginLeft = (int)((Window.ClientBounds.Width - TargetResolution.X * ratio) * 0.5f);
				}
				else
				{
					ratio = (float)Window.ClientBounds.Width / TargetResolution.X;
					marginTop = (int)((Window.ClientBounds.Height - TargetResolution.Y * ratio) * 0.5f);
				}
			}

			DrawTarget = new Rectangle(new Point(marginLeft, marginTop) + GameState.CamShake.Value, new Point((int)(TargetResolution.X * ratio), (int)(TargetResolution.Y * ratio)));

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
			_spriteBatch.Draw(RenderTarget, DrawTarget, Color.White);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
