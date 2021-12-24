using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IParticleGameObject : IGameObject
	{
		float CurrentAlpha { get; set; }
		void Init(Texture2D texture, ITweening tweening, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, float angle, Vector2 rotationOrigin);
	}
}