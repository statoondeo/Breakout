using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class StartedGameplayStateScene : BaseGameplayStateScene
	{
		protected Point Screen;

		public StartedGameplayStateScene(GameplayScene gameplayScene)
			: base(gameplayScene)
		{
			Screen = Services.Instance.Get<IScreenService>().GetScreenSize();
		}

		public override void Enter()
		{
			// Activation des triggers
			IList<IGameObject> triggers = GameplayScene.GetObjects(item => item is BaseTriggerGameObject);
			foreach (IGameObject trigger in triggers)
			{
				trigger.Status = GameObjectStatus.ACTIVE;
			}
		}

		public override void Exit()
		{
			// Désactivation des triggers
			IList<IGameObject> triggers = GameplayScene.GetObjects(item => item is BaseTriggerGameObject);
			foreach (IGameObject trigger in triggers)
			{
				trigger.Status = GameObjectStatus.IDLE;
			}
		}

		public override void Update(GameTime gameTime)
		{
			// Est-ce que le joueur a gagné?
			if (GameplayScene.PlayerWin)
			{
				Services.Instance.Get<ISceneService>().ChangeScene(SceneType.VICTORY, new DummyCommand());
			}

			IGameObject ball = GameplayScene.GetObject(item => item is BallGameObject);
			// Est-ce que le joueur perd 1 vie?
			if ((ball != null) && (ball.Body.Position.Y > Screen.Y))
			{
				GameplayScene.Life--;

				// Est-ce que le joueur a perdu?
				if (GameplayScene.PlayerLoose)
				{
					Services.Instance.Get<ISceneService>().ChangeScene(SceneType.GAMEOVER, new DummyCommand());
				}
				else
				{
					ball.Body.MoveTo(new Vector2(ball.Body.Position.X, Screen.Y - 2 * (ball.Body as ICircleBody).Radius));
					(ball as BallGameObject).Exploded = true;

					// On change d'état : InitializedState
					GameplayScene.GotoState(GameplayStateNames.Initialized);
				}
			}
		}
	}
}
