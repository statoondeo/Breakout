using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameNameSpace
{
	public class GameplayScene : BaseScene, IStateContainer
	{
		protected IStateItem mCurrentState;
		public IStateItem CurrentState 
		{
			get => mCurrentState;
			set
			{
				(mCurrentState as IGameplayStateScene)?.Exit();
				mCurrentState = value;
				(mCurrentState as IGameplayStateScene).Enter();
			}
		}

		public int Life { get; set; }

		public int Level { get; protected set; }

		public override void Load(ICommand commandWhenLoaded)
		{
			Life = 2;
			(CurrentState as IGameplayStateScene).Load();
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

			IStateItem createdState = new CreatedGameplayStateScene(this);
			IStateItem InitializedState = new InitializedGameplayStateScene(this);
			IStateItem startedState = new StartedGameplayStateScene(this);

			createdState.Transitions.Add(InitializedState);
			InitializedState.Transitions.Add(startedState);
			startedState.Transitions.Add(InitializedState);

			CurrentState = createdState;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			(CurrentState as IGameplayStateScene).Update(gameTime);
			if (Services.Instance.Get<IInputListenerService>().IsKeyDown(Keys.Escape))
			{
				(new SwitchSceneCommand(SceneType.MENU)).Execute();
			}
		}
	}
}
