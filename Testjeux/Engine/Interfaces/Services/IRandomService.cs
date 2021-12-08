namespace GameNameSpace
{
	public interface IRandomService : IService
	{
		float Next();
		int Next(int min, int max);
	}
}

