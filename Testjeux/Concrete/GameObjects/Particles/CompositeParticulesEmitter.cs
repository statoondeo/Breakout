using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CompositeParticulesEmitter : IParticlesEmitter
	{
		protected IList<IParticlesEmitter> ParticlesEmittersList;
		protected IGameObject GameObject;

		public CompositeParticulesEmitter(IGameObject gameObject, params IParticlesEmitter[] particlesEmitters)
		{
			ParticlesEmittersList = new List<IParticlesEmitter>();
			GameObject = gameObject;
			if ((null != particlesEmitters) && (particlesEmitters.Length != 0))
			{
				foreach(IParticlesEmitter particlesEmitter in particlesEmitters)
				{
					ParticlesEmittersList.Add(particlesEmitter);
				}
			}
		}

		public void Emit()
		{
			foreach (IParticlesEmitter particlesEmitter in ParticlesEmittersList)
			{
				particlesEmitter.Emit();
			}
		}

		public void Emit(CollisionTestResult collisionResult)
		{
			foreach (IParticlesEmitter particlesEmitter in ParticlesEmittersList)
			{
				particlesEmitter.Emit(collisionResult);
			}
		}

		public void Emit(GameTime gameTime)
		{
			foreach (IParticlesEmitter particlesEmitter in ParticlesEmittersList)
			{
				particlesEmitter.Emit(gameTime);
			}
		}
	}
}