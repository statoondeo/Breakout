namespace GameNameSpace
{
	public interface IBrickGameObject : IGameObject
	{
		int Health { get; set; }
		void Damage();
	}
}