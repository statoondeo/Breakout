namespace GameNameSpace
{
	public interface IBallGameObject : IGameObject
	{
		bool Exploded { get; set; }
		float Speed { get; }
	}
}