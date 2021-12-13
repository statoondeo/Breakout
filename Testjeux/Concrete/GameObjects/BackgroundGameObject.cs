using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BackgroundGameObject : BaseGameObject
	{
		public BackgroundGameObject(Texture2D texture)
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero);
		}
	}
	public class ScrollingBackgroundGameObject : BaseGameObject
	{
		protected IRenderable SecondBackgroundRenderable;

		public ScrollingBackgroundGameObject(Texture2D texture, Vector2 velocity)
			: base()
		{
			Type = GameObjectType.NONE;
			SecondBackgroundRenderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero);
			Renderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero);
			Movable = new VelocityMovable(this);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			SecondBackgroundRenderable.Draw(spriteBatch);
		}
	}
}