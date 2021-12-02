namespace GameNameSpace
{
	public class CircleCollidable : BaseCollidable
	{
		public int Radius { get; protected set; }
		public CircleCollidable(IPositionable positionable, int radius) 
			: base(positionable)
		{
			Type = CollidableType.CIRCLE;
			Radius = radius;
		}
	}
}