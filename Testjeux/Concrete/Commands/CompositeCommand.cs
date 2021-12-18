using System.Collections.Generic;

namespace GameNameSpace
{
	public class CompositeCommand : BaseCommand
	{
		protected IList<ICommand> CommandsList;

		public CompositeCommand(params ICommand[] commands) 
			: base() 
		{
			CommandsList = new List<ICommand>();
			if ((null != commands) && (commands.Length > 0))
			{
				foreach (ICommand command in commands)
				{
					CommandsList.Add(command);
				}
			}
		}

		public override void Execute()
		{
			foreach(ICommand command in CommandsList)
			{
				command.Execute();
			}
		}
	}
}
