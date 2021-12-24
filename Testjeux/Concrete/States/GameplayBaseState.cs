using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public abstract class GameplayBaseState : IGameplayStateScene, IStateItem
	{

		public GameplayBaseState(IStateContainer container)
		{
			Transitions = new List<IStateItem>();
			Container = container;
		}

		public IList<IStateItem> Transitions { get; protected set; }

		public IStateContainer Container { get; protected set; }

		public virtual void Enter() { }
		public virtual void Exit() { }
		public virtual void Load() { }
		public virtual void Update(GameTime gameTime) { }
	}
}
