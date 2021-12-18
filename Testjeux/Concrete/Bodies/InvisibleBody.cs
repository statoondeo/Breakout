using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class InvisibleBody : CircleBody
	{

		public InvisibleBody(Vector2 position)
			: this(position, Vector2.Zero)
		{
		}

		public InvisibleBody(Vector2 position, Vector2 velocity)
			: base(position, 0.0f, velocity, 0.0f, false, new DummyColliderCommand())
		{
		}
	}
}