using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public class GameplayWonState : GameplayBaseState
	{
		public GameplayWonState(IStateContainer gameplayScene)
			: base(gameplayScene)
		{			
		}

		public override void Enter()
		{
			Point screen = Services.Instance.Get<IScreenService>().GetScreenSize();

			ISceneService gameState = Services.Instance.Get<ISceneService>();

			// Désactivation des triggers
			IList<IGameObject> objects = (Container as IScene).GetObjects(item => item is BaseTriggerGameObject);
			foreach (IGameObject trigger in objects)
			{
				trigger.Status = GameObjectStatus.IDLE;
			}

			// Suppression des balles et de la raquette
			objects = (Container as IScene).GetObjects(item => item is RacketGameObject || item is IBallGameObject);
			foreach (IGameObject gameObject in objects)
			{
				gameObject.Status = GameObjectStatus.OUTDATED;
			}

			(Container as GameplayScene).RegisterGameObject(new CursorGameObject());

			IGameObjectFactoryService factory = Services.Instance.Get<IGameObjectFactoryService>();
			Vector2 destination = Vector2.Zero;
			Vector2 origin = new Vector2(destination.X, -800.0f);
			(Container as GameplayScene).RegisterGameObject(factory.DecorateEntrance(new BigPanelGameObject(), origin, destination));

			destination = new Vector2(125.0f, 650.0f);
			origin = new Vector2(destination.X, -300.0f);
			(Container as GameplayScene).RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Menu", Color.Black, new SwitchSceneCommand(SceneType.MENU)), origin, destination));


			SpriteFont font = Services.Instance.Get<IAssetService>().GetFont(FontName.BigTitle);
			Vector2 textSize = font.MeasureString("BRAVO");
			destination = new Vector2((screen.X - textSize.X) * 0.5f, 320);
			origin = new Vector2(destination.X, -300.0f);
			(Container as GameplayScene).RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, font, "BRAVO", Color.Silver), origin, destination));

			if (gameState.Mode == SceneModeNames.Serie)
			{
				if (gameState.Level < Services.Instance.Get<ILevelService>().MaxLevel)
				{
					destination = new Vector2(900.0f, 650.0f);
					origin = new Vector2(destination.X, -300.0f);
					(Container as GameplayScene).RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Continuer", Color.Black, new FollowSerieSceneCommand(SceneType.GAMEPLAY, gameState.Level + 1)), origin, destination));
				}
				else
				{
					font = Services.Instance.Get<IAssetService>().GetFont(FontName.Button);
					textSize = font.MeasureString("Et battu le challenge de SPACE Breaker!");
					destination = new Vector2((screen.X - textSize.X) * 0.5f, 200);
					origin = new Vector2(destination.X, -300.0f);
					(Container as GameplayScene).RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, font, "Et battu le challenge de SPACE Breaker!", Color.Silver), origin, destination));
				}
			}

			font = Services.Instance.Get<IAssetService>().GetFont(FontName.Button);
			textSize = font.MeasureString((Container as GameplayScene).VictoryText);
			destination = new Vector2((screen.X - textSize.X) * 0.5f, 120);
			origin = new Vector2(destination.X, -300.0f);
			(Container as GameplayScene).RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, font, (Container as GameplayScene).VictoryText, Color.Silver), origin, destination));

			MediaPlayer.Stop();
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = 0.5f;
			MediaPlayer.Play(Services.Instance.Get<IAssetService>().GetMusic(MusicName.TheThroneRoom));
		}
	}
}
