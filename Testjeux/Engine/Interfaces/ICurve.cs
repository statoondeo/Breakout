using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public interface ICurve
	{
		float Ttl { get; }
		bool Ended { get; }
		Vector2 Position { get; }
		void Update(GameTime gameTime);
		void Reset();
	}
}