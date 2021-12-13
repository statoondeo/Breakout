using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class DummyParticlesEmitter : IParticlesEmitter
	{
		public DummyParticlesEmitter() { }

		public void Emit() { }
		public void Emit(CollisionTestResult collisionResult) { }
		public void Emit(GameTime gameTime) { }
	}
}