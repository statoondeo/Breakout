namespace GameNameSpace
{
	public class LogService : ILogService
    {
		protected ILogger Logger;

        public LogService(ILogger logger) 
        {
			Logger = logger;
		}

		public string Get()
		{
			return (Logger.Get());
		}

		public void Log(string message)
		{
			Logger.Log(message);
		}

		public void Reset()
		{
			Logger.Reset();
		}
	}
}
