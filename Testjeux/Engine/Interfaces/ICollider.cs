namespace GameNameSpace
{
	public interface ICollider
	{
		CollisionTestResult IsCollision(IBody body1, IBody body2);
		void ResolveCollision(IBody body1, IBody body2, CollisionTestResult collisionResult);
	}
}