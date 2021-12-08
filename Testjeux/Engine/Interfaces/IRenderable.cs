using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IRenderable
	{
		void Draw(SpriteBatch spriteBatch);
		void Draw(SpriteBatch spriteBatch, float alpha, float angle, float scale, Vector2 rotationOrigin);	
	}
}