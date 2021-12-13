using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseRenderable : IRenderable
	{
		public Color ColorMask { get; set; }
		public Vector2 Offset { get; set; }
		public float Scale { get; set; }

		protected BaseRenderable(Vector2 offset, float scale)
		{
			ColorMask = Color.White; 
			Offset = offset; 
			Scale = scale;
		}

		public virtual void Update(GameTime gameTime) { }
		public virtual void Draw(SpriteBatch spriteBatch) { }
		public virtual void Draw(SpriteBatch spriteBatch, float alpha, float angle, float scale, Vector2 rotationOrigin) { }
	}
}