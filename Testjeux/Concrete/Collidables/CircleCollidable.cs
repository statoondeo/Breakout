namespace GameNameSpace
{
	public class CircleCollidable : BaseCollidable
	{
		public CircleCollidable(IPositionable positionable) 
			: base(positionable)
		{
			Type = CollidableType.CIRCLE;
			Renderable = new CircleFrameRenderable(Positionable);
		}
	}
}