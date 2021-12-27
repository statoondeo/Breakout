using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class GameplayLostState : GameplayBaseState
	{

		public GameplayLostState(IStateContainer gameplayScene)
			: base(gameplayScene)
		{
		}

		public override void Enter()
		{
			Point screen = Services.Instance.Get<IScreenService>().GetScreenSize();

			// Désactivation des triggers
			IList<IGameObject> objects = (Container as IScene).GetObjects(item => item is BaseTriggerGameObject);
			foreach (IGameObject trigger in objects)
			{
				trigger.Status = GameObjectStatus.IDLE;
			}

			// Suppression des balles et de la raquette
			objects = (Container as IScene).GetObjects(item => item is RacketGameObject || item is BallGameObject || item is IBrickGameObject);
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

			destination = new Vector2(900.0f, 650.0f);
			origin = new Vector2(destination.X, -300.0f);
			(Container as GameplayScene).RegisterGameObject(factory.DecorateEntrance(new ButtonGameObject(origin, "Rejouer", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY, (Container as GameplayScene).Level)), origin, destination));

			SpriteFont font = Services.Instance.Get<IAssetService>().GetFont(FontName.BigTitle);
			Vector2 textSize = font.MeasureString("DEFAITE");
			destination = new Vector2((screen.X - textSize.X) * 0.5f, 320);
			origin = new Vector2(destination.X, -300.0f);
			(Container as GameplayScene).RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, font, "DEFAITE", Color.Silver), origin, destination));

			font = Services.Instance.Get<IAssetService>().GetFont(FontName.Button);
			textSize = font.MeasureString((Container as GameplayScene).DefeatText);
			destination = new Vector2((screen.X - textSize.X) * 0.5f, 120);
			origin = new Vector2(destination.X, -300.0f);
			(Container as GameplayScene).RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, font, (Container as GameplayScene).DefeatText, Color.Silver), origin, destination));
		}

		public override void Exit()
		{

		}

		public override void Update(GameTime gameTime)
		{
		}
	}
}
