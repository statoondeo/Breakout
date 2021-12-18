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

		public IGameObject Create(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, float angle, Vector2 rotationOrigin, Color maskColor)
		{
			ParticleGameObject particle = ObjectPool.Get();
			particle.Init(texture, tweeningMove, position, velocity, scale, ttl, angleSpeed, initialAlpha, angle, rotationOrigin);
			particle.Renderable.ColorMask = maskColor;
			return (particle);
		}

		public void Register(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, float angle, Vector2 rotationOrigin, Color maskColor)
		{
			Services.Instance.Get<ISceneService>().RegisterGameObject(Create(texture, tweeningMove, position, velocity, scale, ttl, angleSpeed, initialAlpha, angle, rotationOrigin, maskColor));
		}

		public void Register(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, float angle, Vector2 rotationOrigin)
		{
			Register(texture, tweeningMove, position, velocity, scale, ttl, angleSpeed, initialAlpha, angle, rotationOrigin, Color.White);
		}

		public IGameObject Create(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, float angle, Vector2 rotationOrigin)
		{
			return (Create(texture, tweeningMove, position, velocity, scale, ttl, angleSpeed, initialAlpha, angle, rotationOrigin, Color.White));
		}
	}
}