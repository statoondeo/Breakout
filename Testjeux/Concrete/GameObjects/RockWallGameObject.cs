using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class RockWallGameObject : WallGameObject
	{
		public RockWallGameObject(Vector2 position)
			: base(position, new Vector2(64))
		{
			int textureIndex = Services.Instance.Get<IRandomService>().Next(0, 5);
			Texture2D texture = Services.Instance.Get<IShapeService>().CropTexture(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Rocks), new Rectangle(new Point(textureIndex * 64, 0), new Point(64)));
			Renderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero);
		}
	}
}