namespace GameNameSpace
{
	public class DummyCollidable : BaseCollidable
	{
		public DummyCollidable() : base(null) 
		{ 
			Type = CollidableType.NONE;
			Renderable = new DummyRenderable();
		}
	}
}