using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IParticlesService : IService
	{
		void Create(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, Vector2 rotationOrigin);
		void Create(Texture2D texture, ITweening tweeningMove, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, Vector2 rotationOrigin, Color maskColor);
	}
}
