using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseRenderable : IRenderable
	{
		protected BaseRenderable() { }

		public virtual void Draw(SpriteBatch spriteBatch) { }

		public virtual void Draw(SpriteBatch spriteBatch, float alpha, float angle, float scale, Vector2 rotationOrigin) { }
	}
}