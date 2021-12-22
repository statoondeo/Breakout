namespace GameNameSpace
{
	public interface IStateContainer
	{
		IStateItem CurrentState { get; set; }
	}
}