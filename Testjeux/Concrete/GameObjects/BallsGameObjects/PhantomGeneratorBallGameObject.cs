using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class PhantomGeneratorBallGameObject : BallGameObject
	{
		public PhantomGeneratorBallGameObject(Vector2 position, Vector2 velocity, Vector2 size)
			: base(position, velocity, size)
		{
			Body = new BallBody(position, size, velocity, new PhantomGeneratorBallColliderCommand(this));
		}
	}
}