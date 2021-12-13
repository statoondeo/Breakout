using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class ParticleService : IParticlesService
	{
		protected ObjectPool<ParticleGameObject> ObjectPool;

		public ParticleService(int capacity)
		{
			ObjectPool = new ObjectPool<ParticleGameObject>(capacity);
		}

		public void Create(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, Vector2 rotationOrigin)
		{
			Create(texture, tweeningMove, position, velocity, scale, ttl, angleSpeed, initialAlpha, rotationOrigin, Color.White);
		}

		public void Create(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, Vector2 rotationOrigin, Color maskColor)
		{
			ParticleGameObject particle = ObjectPool.Get();
			particle.Init(texture, tweeningMove, position, velocity, scale, ttl, angleSpeed, initialAlpha, rotationOrigin);
			particle.Renderable.ColorMask = maskColor;
			ServiceLocator.Instance.Get<ISceneService>().RegisterGameObject(particle);
		}
	}
}