using System.Diagnostics;

namespace GameNameSpace
{
	public class ConsoleLogger : BaseLogger
	{
		public ConsoleLogger() 
			: base() 
		{
		}

		public override void Log(string message)
		{
			Trace.WriteLine(message);
		}
	}
}