using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class GameplayScene : BaseScene
	{
		protected IGameObject Racket;
		protected IGameObject Ball;
		protected Point Screen;

		protected float Ttl;
		protected bool GameInitialized;
		protected bool GameStarted;
		protected int Level;

		public override void Load()
		{
			base.Load();

			// Chargement du niveau
			RegisterGameObjects(ServiceLocator.Instance.Get<IGameObjectFactoryService>().CreateLevel(ServiceLocator.Instance.Get<ILevelService>().GetLevel(Level)));

			// Raquette du joueur
			Texture2D racketTexture = ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.Platform);
			Vector2 racketSize = new Vector2(racketTexture.Width, racketTexture.Height);
			Vector2 origin = new Vector2((Screen.X - racketSize.X) / 2, -300);
			Vector2 destination = new Vector2((Screen.X - racketSize.X) / 2, Screen.Y - 2 * racketSize.Y);
			Racket = RegisterGameObject(ServiceLocator.Instance.Get<IGameObjectFactoryService>().SetEntranceDecoration(new RacketGameObject(racketTexture, Vector2.Zero, racketSize), origin, destination));

			// Balle de la scène
			origin = new Vector2((Screen.X - 32) / 2, -300);
			destination = new Vector2((Screen.X - 32) / 2, 2 * (Screen.Y - 32) / 3);
			Ball = RegisterGameObject(ServiceLocator.Instance.Get<IGameObjectFactoryService>().SetEntranceDecoration(new BallGameObject(Vector2.Zero, 850, new Vector2(32)), origin, destination));

			Ttl = ServiceLocator.Instance.Get<IGameObjectFactoryService>().MaxTtl;

			GameStarted = false;
			GameInitialized = false;
		}

		public GameplayScene(int level) 
			: base()
		{
			Level = level;
			Screen = ServiceLocator.Instance.Get<IScreenService>().GetScreenSize();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (GameInitialized)
			{
				if (GameStarted)
				{
					// Est-ce que le joueur a gagné?
					if (PlayerWin)
					{
						ServiceLocator.Instance.Get<ISceneService>().ChangeScene(SceneType.VICTORY);
					}

					// Est-ce que le joueur a perdu?
					if (Ball.Body.Position.Y > Screen.Y)
					{
						Ball.Body.MoveTo(new Vector2(Ball.Body.Position.X, Screen.Y - 2 * (Ball.Body as ICircleBody).Radius));
						(Ball as BallGameObject).Exploded = true;

						// Balle de la scène
						Vector2 origin = new Vector2((Screen.X - 32) / 2, -64);
						Vector2 destination = new Vector2((Screen.X - 32) / 2, 2 * (Screen.Y - 32) / 3);
						Ball = RegisterGameObject(ServiceLocator.Instance.Get<IGameObjectFactoryService>().SetEntranceDecoration(new BallGameObject(Vector2.Zero, 850, new Vector2(32)), origin, destination));
						GameStarted = false;
					}
				}
				else
				{
					if ((GameStarted = ServiceLocator.Instance.Get<IInputListenerService>().IsLeftClick()) && (Ball is IGameObjectDecorator))
					{
						// Suppression du décorateur de la balle
						Ball.Status = GameObjectStatus.OUTDATED;
						Ball = (Ball as IGameObjectDecorator).DecoratedGameObject;
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
						decoratedGameObject.Status = GameObjectStatus.OUTDATED;
						RegisterGameObject(decoratedGameObject.DecoratedGameObject);
						if (decoratedGameObject == Racket)
						{
							Racket = decoratedGameObject.DecoratedGameObject;
						}
					}
				}
			}
		}
	}
}
