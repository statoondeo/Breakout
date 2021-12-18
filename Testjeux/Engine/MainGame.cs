using System;
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
			GameNameSpace.Services.Instance.Register<ILogService>(new LogService(new ConsoleLogger()));
			GameNameSpace.Services.Instance.Register<IScreenService>(new ScreenService(Window.ClientBounds));
			GameNameSpace.Services.Instance.Register<IShapeService>(new ShapeService(_spriteBatch));
			GameNameSpace.Services.Instance.Register<IRandomService>(new RandomService());
			GameNameSpace.Services.Instance.Register<IColliderService>(new ColliderService());
			GameNameSpace.Services.Instance.Register<ITweeningService>(new TweeningService());
			GameNameSpace.Services.Instance.Register<IParticlesService>(new ParticleService(250));
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

			// TODO: use this.Content to load your game content here

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

			// TODO: Add your update logic here
			GameState.Update(gameTime);
			InputListener.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// TODO: Add your drawing code here
			_spriteBatch.Begin();
			GameState.Draw(_spriteBatch);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
