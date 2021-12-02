using System.Linq;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class GameplayScene : BaseScene
	{
		private static readonly Point BRICKS = new Point(8, 6);

		protected ICommand WinCommand;
		protected ICommand LooseCommand;

		protected IGameObject Ball;
		protected Rectangle Screen;

		public GameplayScene() : base()
		{
			Screen = ServiceLocator.Instance.Get<Game>().Window.ClientBounds;

			// Condition de victoires et défaites
			WinCommand = new SwitchSceneCommand(new GotoSceneCommand(GameState.SceneType.VICTORY));
			LooseCommand = new SwitchSceneCommand(new GotoSceneCommand(GameState.SceneType.GAMEOVER));

			// Murs autour de la scène
			RegisterGameObject(new WallGameObject(new Vector2(-50), new Point(Screen.Width + 100, 50)));
			RegisterGameObject(new WallGameObject(new Vector2(-50, 0), new Point(50, Screen.Height)));
			RegisterGameObject(new WallGameObject(new Vector2(Screen.Width, 0), new Point(50, Screen.Height)));

			// Briques de la scène
			Point brickWrapper = new Point(Screen.Width / BRICKS.X, Screen.Height / 3 / BRICKS.Y);
			Point brickSize = new Point(brickWrapper.X - 2, brickWrapper.Y - 2);
			for (int i = 0; i < BRICKS.X; i++)
			{
				for (int j = 0; j < BRICKS.Y; j++)
				{
					RegisterGameObject(new BrickGameObject(new Vector2(i * brickWrapper.X + 1, j * brickWrapper.Y + 1), brickSize));
				}
			}

			// Raquette du joueur
			IGameObject racket = RegisterGameObject(new RacketGameObject(new Vector2((Screen.Width - RacketGameObject.DEFAULT_SIZE.X) / 2, Screen.Height - 2 * RacketGameObject.DEFAULT_SIZE.Y)));

			// Balle de la scène
			Ball = RegisterGameObject(new BallGameObject(new Vector2(racket.Movable.Position.X + (racket.Movable.Size.X - BallGameObject.DEFAULT_SIZE.X) / 2, racket.Movable.Position.Y - BallGameObject.DEFAULT_SIZE.Y), new Vector2(300, -300)));
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			// Est-ce que le joueur a gagné?
			if (GameObjectsCollection.Count(gameObject => gameObject.Type == GameObjectType.BRICK) == 0)
			{
				WinCommand.Execute();
			}

			// Est-ce que le joueur a perdu?
			if (Ball.Movable.Position.Y > Screen.Height)
			{
				LooseCommand.Execute();
			}
		}
	}
}
