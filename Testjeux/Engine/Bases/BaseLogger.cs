namespace GameNameSpace
{
	public abstract class BaseLogger : ILogger
	{
		protected BaseLogger() { }

		public virtual void Log(string message) { }
		public virtual void Reset() { }
		public virtual string Get() { return (string.Empty); }
	}
}
