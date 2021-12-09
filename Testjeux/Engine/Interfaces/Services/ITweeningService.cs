namespace GameNameSpace
{
	public interface ITweeningService : IService
	{
		ITweening Get(TweeningName name);
	}
}