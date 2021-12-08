using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class MainGame : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private GameState GameState;
		private InputListener InputListener;

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();

			// Changement de la résolution du jeu
			_graphics.PreferredBackBufferWidth = 800;
			_graphics.PreferredBackBufferHeight = 600;
			_graphics.ApplyChanges();

			// Enregistrement des services
			ServiceLocator.Instance.Register<Game>(this);
			ServiceLocator.Instance.Register<SpriteBatch>(_spriteBatch);
			ServiceLocator.Instance.Register<ShapeFactory>(new ShapeFactory());
			ServiceLocator.Instance.Register<Random>(new Random());
			ServiceLocator.Instance.Register<ParticleService>(new ParticleService(250));
			InputListener = ServiceLocator.Instance.Register<InputListener>(new InputListener());
			GameState = ServiceLocator.Instance.Register<GameState>(new GameState());

			// Redirection vers la 1ere scène du jeu
			GameState.ChangeScene(SceneType.MENU);
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here

			// Chargement des ressources
			ServiceLocator.Instance.Register<AssetManager>(new AssetManager()).Load(Content, _spriteBatch.GraphicsDevice);
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();
			ServiceLocator.Instance.Get<AssetManager>().UnLoad();
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
