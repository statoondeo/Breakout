using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class InvisibleBodyWrapper : InvisibleBody
	{
		protected IBody Body;

		public InvisibleBodyWrapper(IBody body) 
			: base(body.Position)
		{
			Body = body;
		
		}
		public override void Move(Vector2 offset)
		{
			Body.Move(offset);
		}

		public override void MoveTo(Vector2 newPosition)
		{
			Body.MoveTo(newPosition);
		}
	}
}