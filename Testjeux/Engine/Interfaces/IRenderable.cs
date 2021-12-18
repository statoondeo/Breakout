using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IRenderable
	{
		Color ColorMask { get; set; }
		Vector2 Offset { get; set; }
		float Scale { get; set; }
		float Alpha { get; set; }
		public float Layer { get; set; }
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch);
		void Draw(SpriteBatch spriteBatch, float alpha, float angle, float scale, Vector2 rotationOrigin);	
	}
}