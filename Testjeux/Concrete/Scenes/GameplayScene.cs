using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class GameplayScene : BaseScene
	{
		private static readonly Point BRICKS = new Point(5);
		private static readonly Point BALL = new Point(24);
		private static readonly Point RACKET = new Point(120, 20);

		protected ICommand WinCommand;
		protected ICommand LooseCommand;

		protected IGameObject Racket;
		protected IGameObject Ball;
		protected Point Screen;

		protected float Ttl;
		protected bool GameInitialized;
		protected bool GameStarted;

		public GameplayScene() : base()
		{
			Screen = ServiceLocator.Instance.Get<IScreenService>().GetScreenSize();

			// Condition de victoires et défaites
			WinCommand = new SwitchSceneCommand(SceneType.VICTORY);
			LooseCommand = new SwitchSceneCommand(SceneType.GAMEOVER);

			// Murs autour de la scène
			RegisterGameObject(new WallGameObject(new Vector2(-50), new Vector2(Screen.X + 100, 50)));
			RegisterGameObject(new WallGameObject(new Vector2(-50, 0), new Vector2(50, Screen.Y)));
			RegisterGameObject(new WallGameObject(new Vector2(Screen.X, 0), new Vector2(50, Screen.Y)));

			// Briques de la scène
			Vector2 originOffset = Vector2.Zero;
			Vector2 destinationOffset = new Vector2(0, Screen.Y / 5);
			Point brickWrapper = new Point(Screen.X / BRICKS.X / 3, Screen.Y / 5 / BRICKS.Y);
			Point brickSize = new Point(brickWrapper.X - 2, brickWrapper.Y - 2);
			Ttl = 0;
			Vector2 origin, destination;
			for (int i = 1; i < BRICKS.X - 1; i++)
			{
				for (int j = 1; j < BRICKS.Y - 1; j++)
				{
					origin = new Vector2(i * brickWrapper.X + 1, -2 * brickSize.Y) + originOffset;
					destination = new Vector2(i * brickWrapper.X + 1, j * brickWrapper.Y + 1) + destinationOffset;
					RegisterGameObject(SetEntranceDecoration(new BrickGameObject(Vector2.Zero, brickSize.ToVector2(), 1), origin, destination));
}
}

			originOffset = new Vector2(2 * Screen.X / 3, 0);
			destinationOffset = new Vector2(2 * Screen.X / 3, Screen.Y / 5);
			for (int i = 1; i < BRICKS.X - 1; i++)
			{
				for (int j = 1; j < BRICKS.Y - 1; j++)
				{
					origin = new Vector2(i * brickWrapper.X + 1, -2 * brickSize.Y) + originOffset;
					destination = new Vector2(i * brickWrapper.X + 1, j * brickWrapper.Y + 1) + destinationOffset;
					RegisterGameObject(SetEntranceDecoration(new BrickGameObject(Vector2.Zero, brickSize.ToVector2(), 1), origin, destination));
				}
			}
			origin = new Vector2((Screen.X - 200) * 0.5f, -200);
			destination = new Vector2((Screen.X - 200) * 0.5f, (Screen.Y - 200) * 0.25f);
			RegisterGameObject(SetEntranceDecoration(new BumperGameObject(Vector2.Zero), origin, destination));

			// Raquette du joueur
			origin = new Vector2((Screen.X - RACKET.X) / 2, Screen.Y + 2 * RACKET.Y);
			destination = new Vector2((Screen.X - RACKET.X) / 2, Screen.Y - 2 * RACKET.Y);
			Racket = RegisterGameObject(SetEntranceDecoration(new RacketGameObject(Vector2.Zero, RACKET.ToVector2()), origin, destination));

			// Balle de la scène
			origin = new Vector2((Screen.X - BALL.X) / 2, Screen.Y + 2 * BALL.Y);
			destination = new Vector2((Screen.X - BALL.X) / 2, 2 * (Screen.Y - BALL.Y) / 3);
			Ball = RegisterGameObject(SetEntranceDecoration(new BallGameObject(new Vector2((Screen.X - BALL.X) / 2, Screen.Y + 2 * BALL.Y), 850, BALL.ToVector2()), origin, destination));

			GameStarted = false;
			GameInitialized = false;
		}

		protected IGameObject SetEntranceDecoration(IGameObject gameObject, Vector2 origin, Vector2 destination)
{
			float fallTtl = ServiceLocator.Instance.Get<IRandomService>().Next() * 0.6f + 0.4f;
			float delay = ServiceLocator.Instance.Get<IRandomService>().Next() * 0.8f + 0.2f;
			Ttl = Math.Max(Ttl, fallTtl + delay);
			return (new ElasticEntranceDecorator(gameObject, origin, destination, fallTtl, delay));
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (GameInitialized)
			{
				if (GameStarted)
				{
					//// Est-ce que le joueur a gagné?
					//if (GameObjectsCollection.Count(gameObject => gameObject.Type == GameObjectType.BRICK) == 0)
					//{
					//	WinCommand.Execute();
					//}

					// Est-ce que le joueur a perdu?
					if (Ball.Body.Position.Y > Screen.Y)
					{
						Ball.Body.MoveTo(new Vector2(Ball.Body.Position.X, Screen.Y - 2 * (Ball.Body as ICircleBody).Radius));
						(Ball as BallGameObject).Exploded = true;

						//LooseCommand.Execute();
					}
				}
				else
				{
					if (GameStarted = ServiceLocator.Instance.Get<IInputListenerService>().IsLeftClick())
					{
						// Suppression du décorateur de la balle
						IGameObjectDecorator ballDecorator = Ball as IGameObjectDecorator;
						Ball = ballDecorator.DecoratedGameObject;
						ballDecorator.Status = GameObjectStatus.OUTDATED;
						RegisterGameObject(Ball);
					}
				}
			}
			else
			{
				Ttl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (Ttl < 0)
				{
					GameInitialized = true;
					// On supprime tous les décorateurs des objets décorés sauf celui de la balle
					// (les murs ne sont pas décorés)
					foreach (IGameObjectDecorator decoratedGameObject in GameObjectsCollection.Where(item => item != Ball && item is IGameObjectDecorator))
					{
						IGameObject nudeGameObject = decoratedGameObject.DecoratedGameObject;
						decoratedGameObject.Status = GameObjectStatus.OUTDATED;
						RegisterGameObject(nudeGameObject);
						if (nudeGameObject == Racket)
						{
							Racket = nudeGameObject;
						}
					}
				}
			}
		}
	}
}
