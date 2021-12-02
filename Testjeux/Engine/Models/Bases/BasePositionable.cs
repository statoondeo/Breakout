using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public abstract class BasePositionable : IPositionable
	{
		public Vector2 Position { get; set; }
		public Point Size { get; set; }

		protected BasePositionable(Vector2 position, Point size) 
		{
			Position = position;
			Size = size;
		}
	}
}