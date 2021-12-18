using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameNameSpace
{
	public class GameplayScene : BaseScene
	{
		protected IDictionary<GameplayStateNames, BaseGameplayStateScene> GameplayStates;
		protected IGameplayStateScene CurrentState;

		public int Life { get; set; }

		public int Level { get; protected set; }

		public override void Load(ICommand commandWhenLoaded)
		{
			Life = 2;
			CurrentState.Load();
			RegisterGameObject(new InScreenTransitionGameObject(new CompositeCommand(commandWhenLoaded, new ResetTransitionRequiredCommand())));
		}

		public override void UnLoad(ICommand commandWhenUnloaded)
		{
			RegisterGameObject(new OutScreenTransitionGameObject(new CompositeCommand(commandWhenUnloaded, new ResetTransitionRequiredCommand())));
		}

		public GameplayScene(int level) 
			: base()
		{
			Level = level;
			GameplayStates = new Dictionary<GameplayStateNames, BaseGameplayStateScene>
			{
				{ GameplayStateNames.Created, new CreatedGameplayStateScene(this) },
				{ GameplayStateNames.Initialized, new InitializedGameplayStateScene(this) },
				{ GameplayStateNames.Started, new StartedGameplayStateScene(this) }
			};
			GotoState(GameplayStateNames.Created);
		}

		public void GotoState(GameplayStateNames newState)
		{
			CurrentState?.Exit();
			CurrentState = GameplayStates[newState];
			CurrentState.Enter();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			CurrentState.Update(gameTime);
			if (Services.Instance.Get<IInputListenerService>().IsKeyDown(Keys.Escape))
			{
				(new SwitchSceneCommand(SceneType.MENU)).Execute();
			}
		}
	}
}
