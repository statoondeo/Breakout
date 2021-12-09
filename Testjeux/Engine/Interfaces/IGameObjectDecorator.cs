namespace GameNameSpace
{
	public interface IGameObjectDecorator : IGameObject
	{
		IGameObject DecoratedGameObject { get; }
	}
}