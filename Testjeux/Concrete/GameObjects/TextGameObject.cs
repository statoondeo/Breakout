using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;

namespace GameNameSpace
{
	public class BrainMiniatureGameObject : BaseGameObject
	{
		public BrainMiniatureGameObject(Vector2 position)
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new BrainIdleRenderable(this, 0.5f, Vector2.Zero)
			{
				Layer = 0.81f
			};
			Body.MoveTo(position);
		}
	}
	public class BlobMiniatureGameObject : BaseGameObject
	{
		public BlobMiniatureGameObject(Vector2 position)
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new BlobAnimatedTextureRenderable(this, 1.3f)
			{
				Layer = 0.81f
			};
			Body.MoveTo(position);
		}
	}
	public class SnakeMiniatureGameObject : BaseGameObject
	{
		public SnakeMiniatureGameObject(Vector2 position)
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new SnakeHeadRenderable(this, 1.2f)
			{
				Layer = 0.81f
			};
			Body.MoveTo(position);
		}
	}
	public class TextGameObject : BaseGameObject
	{
		public TextGameObject(Vector2 position, SpriteFont spriteFont, string text, Color textColor)
			: this(position, spriteFont, text, textColor, 0.0f)
		{
		}

		public TextGameObject(Vector2 position, SpriteFont spriteFont, string text, Color textColor, float angle)
		{
			Body = new InvisibleBody(position);
			Renderable = new TextRenderable(this, Vector2.Zero, spriteFont, text, textColor)
			{
				Layer = 0.75f
			};
			Body.Angle = angle;
		}
	}
}