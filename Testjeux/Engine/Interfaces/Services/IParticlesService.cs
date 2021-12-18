using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IParticlesService : IService
	{
		IGameObject Create(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, float angle, Vector2 rotationOrigin);
		IGameObject Create(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, float angle, Vector2 rotationOrigin, Color maskColor);
		void Register(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, float angle , Vector2 rotationOrigin);
		void Register(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, float angle, Vector2 rotationOrigin, Color maskColor);
	}
}
