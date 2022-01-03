using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public sealed class GameplayScene : BaseScene, IStateContainer
	{
		private IStateItem mCurrentState;
		public Song Music { get; set; }
		public ParsedLevel Level { get; set; }
		public string VictoryText { get; set; }
		public string DefeatText { get; set; }

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

		public override void Load(ICommand commandWhenLoaded)
		{
			base.Load(commandWhenLoaded);
			(CurrentState as IGameplayStateScene).Load();
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = 0.5f;
			MediaPlayer.Play(Music);
			RegisterGameObject(new InScreenTransitionGameObject(new CompositeCommand(commandWhenLoaded, new ResetTransitionRequiredCommand())));
		}

		public override void UnLoad(ICommand commandWhenUnloaded)
		{
			RegisterGameObject(new OutScreenTransitionGameObject(new CompositeCommand(commandWhenUnloaded, new ResetTransitionRequiredCommand())));
			MediaPlayer.Stop();
		}

		public GameplayScene(ParsedLevel level)
			: this(0)
		{
			Level = level;
			Services.Instance.Get<ISceneService>().Mode = SceneModeNames.Selection;
			Services.Instance.Get<ISceneService>().Life = Services.Instance.Get<ISceneService>().MaxLife;
		}

		public GameplayScene(int level)
			: base()
		{
			Level = null;
			Services.Instance.Get<ISceneService>().Level = level;

			IStateItem createdState = new GameplayCreatedState(this);
			IStateItem InitializedState = new GameplayInitializedState(this);
			IStateItem startedState = new GameplayStartedState(this);
			IStateItem wonState = new GameplayWonState(this);
			IStateItem lostState = new GameplayLostState(this);

			createdState.Transitions.Add(InitializedState);
			InitializedState.Transitions.Add(startedState);
			startedState.Transitions.Add(InitializedState);
			startedState.Transitions.Add(wonState);
			startedState.Transitions.Add(lostState);

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
			if (Services.Instance.Get<IInputListenerService>().IsKeyPressed(Keys.F2))
			{
				(new WinTriggerCommand()).Execute();
			}
			if (Services.Instance.Get<IInputListenerService>().IsKeyPressed(Keys.F3))
			{
				(new LooseTriggerCommand()).Execute();
			}
		}
	}
}
