using System.Linq;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class GameplayScene : BaseScene
	{
		private static readonly Point BRICKS = new Point(8, 6);
		private static readonly Point BALL = new Point(24);
		private static readonly Point RACKET = new Point(120, 20);

		protected ICommand WinCommand;
		protected ICommand LooseCommand;

		protected IGameObject Ball;
		protected Rectangle Screen;

		public GameplayScene() : base()
		{
			Screen = ServiceLocator.Instance.Get<Game>().Window.ClientBounds;

			// Condition de victoires et défaites
			WinCommand = new SwitchSceneCommand(SceneType.VICTORY);
			LooseCommand = new SwitchSceneCommand(SceneType.GAMEOVER);

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
			IGameObject racket = RegisterGameObject(new RacketGameObject(new Vector2((Screen.Width - RACKET.X) / 2, Screen.Height - 2 * RACKET.Y), RACKET));

			// Balle de la scène
			Ball = RegisterGameObject(new BallGameObject(new Vector2(racket.Movable.Position.X + (racket.Movable.Size.X - BALL.X) / 2, racket.Movable.Position.Y - BALL.Y), new Vector2(300, -300), BALL));
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
