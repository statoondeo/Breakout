using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameNameSpace
{
	public class GameplayStartedState : GameplayBaseState
	{
		protected Point Screen;

		public GameplayStartedState(IStateContainer gameplayScene)
			: base(gameplayScene)
		{
			Screen = Services.Instance.Get<IScreenService>().GetScreenSize();
		}

		public override void Enter()
		{
			// Activation des triggers
			IList<IGameObject> triggers = (Container as IScene).GetObjects(item => item is BaseTriggerGameObject);
			foreach (IGameObject trigger in triggers)
			{
				trigger.Status = GameObjectStatus.ACTIVE;
			}
		}

		public override void Exit()
		{
			// Désactivation des triggers
			IList<IGameObject> triggers = (Container as IScene).GetObjects(item => item is BaseTriggerGameObject);
			foreach (IGameObject trigger in triggers)
			{
				trigger.Status = GameObjectStatus.IDLE;
			}
		}

		public override void Update(GameTime gameTime)
		{
			// Est-ce que le joueur a gagné?
			if ((Container as GameplayScene).PlayerWin)
			{
				// On change d'état : WonState
				Container.CurrentState = this.Transitions[1];
			}
			else
			{
				IList<IGameObject> balls = (Container as IScene).GetObjects(item => item is IBallGameObject);

				if (balls.Count == 0)
				{
					// Est-ce que le joueur a perdu?
					if ((Container as GameplayScene).PlayerLoose)
					{
						// On change d'état : LostState
						Container.CurrentState = this.Transitions[2];
					}
					else
					{
						// On change d'état : InitializedState
						Container.CurrentState = this.Transitions[0];
					}
				}
				else
				{
					foreach (IGameObject ball in balls)
					{
						if (ball.Body.Position.Y > Screen.Y)
						{
							(Container as GameplayScene).RegisterGameObject(new FlashScreenGameObject());
							Services.Instance.Get<IAssetService>().GetSound(SoundName.Explosion3).Play();
							ball.Body.MoveTo(new Vector2(ball.Body.Position.X, Screen.Y - 2 * (ball.Body as ICircleBody).Radius));
							(ball as IBallGameObject).Exploded = true;
						}
					}
				}
			}

			// Raccourcis de triche
			if (Services.Instance.Get<IInputListenerService>().IsKeyPressed(Keys.A))
			{
				// On recherche la balle et on la décore en multi
				IGameObject ball = (Container as IScene).GetObject(item => item is IBallGameObject);
				(Container as IScene).UnRegisterGameObject(ball);
				(Container as IScene).RegisterGameObject(new MultiBallGameObject(ball));
			}
		}
	}
}
