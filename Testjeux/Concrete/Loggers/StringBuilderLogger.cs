using System.Text;

namespace GameNameSpace
{
	public class StringBuilderLogger : BaseLogger
	{
		protected StringBuilder Content;
		
		public StringBuilderLogger() 
			: base() 
		{
			Content = new StringBuilder();
		}

		public override string Get()
		{
			return (Content.ToString());
		}

		public override void Log(string message)
		{
			Content.AppendLine(message);
		}

		public override void Reset()
		{
			Content.Clear();
		}
	}
}