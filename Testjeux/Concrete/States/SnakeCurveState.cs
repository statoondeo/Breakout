using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class SnakeCurveState : CompositeCurve, IStateItem
	{
		public IList<IStateItem> Transitions { get; protected set; }
		public IStateContainer Container { get; protected set; }

		public SnakeCurveState(IStateContainer container, params ICurve[] curves) 
			: base(false, curves)
		{
			Transitions = new List<IStateItem>();
			Container = container;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (Ended)
			{
				if (Transitions.Count > 1)
				{
					Container.CurrentState = Transitions[Services.Instance.Get<IRandomService>().Next(0, Transitions.Count - 1)];
				}
				else
				{
					Container.CurrentState = Transitions[0];
				}
				(Container.CurrentState as ICurve).Reset();
			}
		}

		public void Enter() { }

		public void Exit() { }
	}
}