using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class InitializedGameplayStateScene : BaseGameplayStateScene
	{
		protected IGameObject MessageGameObject;
		protected IGameObject DecoratedMessageGameObject;
		protected Vector2 OutScreenPosition;
		protected Vector2 OnScreenPosition;
		protected float Ttl;

		public InitializedGameplayStateScene(GameplayScene gameplayScene)
			: base(gameplayScene)
		{
			string label = "Cliquez pour démarrer";
			SpriteFont font = Services.Instance.Get<IAssetService>().GetFont(FontName.Button);
			Vector2 size = font.MeasureString(label);
			Vector2 screen = Services.Instance.Get<IScreenService>().GetScreenSize().ToVector2();
			OnScreenPosition = new Vector2((screen.X - size.X) * 0.5f, (screen.X - size.X) / 3.0f);
			OutScreenPosition = new Vector2(OnScreenPosition.X, -300);
			MessageGameObject = new TextGameObject(OutScreenPosition, size, font, label, Color.White);
			Ttl = 0.25f;    
		}

		public override void Enter()
		{
			if (DecoratedMessageGameObject != null)
			{
				DecoratedMessageGameObject.Status = GameObjectStatus.OUTDATED;
			}
			DecoratedMessageGameObject = GameplayScene.RegisterGameObject(new TweenMoveDecorator(MessageGameObject, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), OutScreenPosition, OnScreenPosition, 0.25f, 0.0f));
			// Balle de la scène
			Vector2 screen = Services.Instance.Get<IScreenService>().GetScreenSize().ToVector2();
			Vector2 origin = new Vector2((screen.X - 32) / 2, -300);
			Vector2 destination = new Vector2((screen.X - 32) / 2, 2 * (screen.Y - 32) / 3);
			IGameObject ball = new InvisibleBodyDecorator(new BallGameObject(Vector2.Zero, 850, new Vector2(32)));
			GameplayScene.RegisterGameObject(new TweenMoveDecorator(new WaitClickDecoratorGameObject(ball, new RemoveInvisibleBodyDecoratorCommand(ball)), Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), origin, destination, 0.25f, 0.0f));
		}

		public override void Exit()
		{
			DecoratedMessageGameObject.Status = GameObjectStatus.OUTDATED;
			DecoratedMessageGameObject = GameplayScene.RegisterGameObject(new TweenMoveDecorator(MessageGameObject, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintIn), OnScreenPosition, OutScreenPosition, 0.25f, 0.0f));
		}

		public override void Update(GameTime gameTime)
		{
			if (Ttl == 0)
			{
				if (Services.Instance.Get<IInputListenerService>().IsLeftClick())
				{
					// On change d'état : StartedState
					GameplayScene.GotoState(GameplayStateNames.Started);
				}
			}
			else
			{
				Ttl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (Ttl < 0)
				{
					Ttl = 0;
				}
			}
		}
	}
}
