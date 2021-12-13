using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class MainGame : Game
	{
		private readonly GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private ISceneService GameState;
		private IInputListenerService InputListener;

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			_graphics.GraphicsProfile = GraphicsProfile.HiDef;
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();

			// Changement de la résolution du jeu
			_graphics.PreferredBackBufferWidth = 1280;
			_graphics.PreferredBackBufferHeight = 800;
			_graphics.ApplyChanges();

			// Enregistrement des services
			ServiceLocator.Instance.Register<IScreenService>(new ScreenService(Window.ClientBounds));
			ServiceLocator.Instance.Register<IShapeService>(new ShapeService(_spriteBatch));
			ServiceLocator.Instance.Register<IRandomService>(new RandomService());
			ServiceLocator.Instance.Register<ITweeningService>(new TweeningService());
			ServiceLocator.Instance.Register<IParticlesService>(new ParticleService(250));
			ServiceLocator.Instance.Register<IGameObjectFactoryService>(new GameObjectFactoryService());
			ServiceLocator.Instance.Register<ILevelService>(new JSONLevelService());
			InputListener = ServiceLocator.Instance.Register<IInputListenerService>(new InputListenerService());
			GameState = ServiceLocator.Instance.Register<ISceneService>(new SceneService());

			// Redirection vers la 1ere scène du jeu
			GameState.ChangeScene(SceneType.MENU);
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here

			// Chargement des ressources
			ServiceLocator.Instance.Register<IAssetService>(new AssetService()).Load(Content, _spriteBatch.GraphicsDevice);
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
			ServiceLocator.Instance.Get<IAssetService>().UnLoad();
		}

		protected override void Update(GameTime gameTime)
		{
			//if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			//	Exit();

			// TODO: Add your update logic here
			GameState.Update(gameTime);
			InputListener.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.LightGray);

			// TODO: Add your drawing code here
			_spriteBatch.Begin();
			GameState.Draw(_spriteBatch);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
