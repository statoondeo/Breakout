using System;

namespace GameNameSpace
{
	public class GenericCommand : BaseCommand
	{
		protected Action<object> ActionToExecute;

		public GenericCommand(Action<object> actionToExecute)
			: base()
		{
			ActionToExecute = actionToExecute;
		}

		public override void Execute()
		{
			ActionToExecute(null);
		}
	}
}
