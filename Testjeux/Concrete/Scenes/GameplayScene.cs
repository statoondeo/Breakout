﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public sealed class GameplayScene : BaseScene, IStateContainer
	{
		private IStateItem mCurrentState;
		public Song Music { get; set; }

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

		public int Level { get; private set; }

		public override void Load(ICommand commandWhenLoaded)
		{
			Life = 2;
			base.Load(commandWhenLoaded);
			(CurrentState as IGameplayStateScene).Load();
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(Music);
			RegisterGameObject(new InScreenTransitionGameObject(new CompositeCommand(commandWhenLoaded, new ResetTransitionRequiredCommand())));
		}

		public override void UnLoad(ICommand commandWhenUnloaded)
		{
			RegisterGameObject(new OutScreenTransitionGameObject(new CompositeCommand(commandWhenUnloaded, new ResetTransitionRequiredCommand())));
			MediaPlayer.Stop();
		}

		public GameplayScene(int level) 
			: base()
		{
			Level = level;

			IStateItem createdState = new GameplayCreatedState(this);
			IStateItem InitializedState = new GameplayInitializedState(this);
			IStateItem startedState = new GameplayStartedState(this);

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
