using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public interface IParticlesEmitter
	{
		void Emit();
		void Emit(CollisionTestResult collisionResult);
		void Emit(GameTime gameTime);
	}
}