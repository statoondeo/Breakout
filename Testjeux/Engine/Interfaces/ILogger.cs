namespace GameNameSpace
{
	public interface ILogger
	{
		void Log(string message);
		string Get();
		void Reset();
	}
}