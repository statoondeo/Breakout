namespace GameNameSpace
{
	public interface ICompositeIntersecBody : IBody
	{
		IBody CollisionCheckerBody { get; }
		IBody CollisionResolverBody { get; }
	}
}
