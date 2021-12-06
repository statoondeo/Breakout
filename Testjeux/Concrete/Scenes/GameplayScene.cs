using System;
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

		protected IGameObject Racket;
		protected IGameObject Ball;
		protected Rectangle Screen;

		public GameplayScene() : base()
		{
			Screen = ServiceLocator.Instance.Get<Game>().Window.ClientBounds;

			// Condition de victoires et défaites
			WinCommand = new SwitchSceneCommand(SceneType.VICTORY);
			LooseCommand = new SwitchSceneCommand(SceneType.GAMEOVER);

			// Murs autour de la scène
			RegisterGameObject(new WallGameObject(new Vector2(-50), new Vector2(Screen.Width + 100, 50)));
			//RegisterGameObject(new WallGameObject(new Vector2(-50, Screen.Height), new Vector2(Screen.Width + 100, 50)));
			RegisterGameObject(new WallGameObject(new Vector2(-50, 0), new Vector2(50, Screen.Height)));
			RegisterGameObject(new WallGameObject(new Vector2(Screen.Width, 0), new Vector2(50, Screen.Height)));

			//Random rand = new Random();
			//for (int i = 0; i < 100; i++)
			//{
			//	RegisterGameObject(new BallGameObject(new Vector2((float)rand.NextDouble() * 600 + 100, (float)rand.NextDouble() * 400 + 100), new Vector2((float)rand.NextDouble() * 600 - 300, (float)rand.NextDouble() * 600 - 300), BALL.ToVector2()));
			//}

			// Briques de la scène
			Point brickWrapper = new Point(Screen.Width / BRICKS.X, Screen.Height / 3 / BRICKS.Y);
			Point brickSize = new Point(brickWrapper.X - 2, brickWrapper.Y - 2);
			for (int i = 0; i < BRICKS.X; i++)
			{
				for (int j = 0; j < BRICKS.Y; j++)
				{
					RegisterGameObject(new BrickGameObject(new Vector2(i * brickWrapper.X + 1, j * brickWrapper.Y + 1), brickSize.ToVector2()));
				}
			}

			// Raquette du joueur
			Racket = RegisterGameObject(new RacketGameObject(new Vector2((Screen.Width - RACKET.X) / 2, Screen.Height - 2 * RACKET.Y), RACKET.ToVector2()));

			// Balle de la scène
			Ball = RegisterGameObject(new BallGameObject(new Vector2(Racket.Body.Position.X + (RACKET.X - BALL.X ) / 2, Racket.Body.Position.Y - BALL.Y), new Vector2(200, -700), BALL.ToVector2()));
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
			if (Ball.Body.Position.Y > Screen.Height)
			{
				LooseCommand.Execute();
			}
		}
	}
}
