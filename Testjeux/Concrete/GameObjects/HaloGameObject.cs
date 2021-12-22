using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class HaloGameObject : IGameObject
	{
		public HaloGameObject(Color colorMask)
		{
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
			Renderable = new TextureRenderable(this, texture, 1.0f, new Vector2(texture.Width, texture.Height) * -0.5f)
			{
				ColorMask = colorMask,
				Layer = 0.4f
			};
			Body = new InvisibleBody(Vector2.Zero);
		}

		public GameObjectStatus Status { get; set; }

		public GameObjectType Type => GameObjectType.NONE;

		public IMovable Movable { get; set; }
		public IBody Body { get; set; }
		public IRenderable Renderable { get; set; }

		public void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch);
		}

		public void Update(GameTime gameTime)
		{
		}
	}
}