using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class WallGameObject : BaseGameObject
	{
		public WallGameObject(Vector2 position, Vector2 size)
			: base()
		{
			Type = GameObjectType.WALL;
			int textureIndex = Services.Instance.Get<IRandomService>().Next(0, 5);
			Texture2D texture = Services.Instance.Get<IShapeService>().CropTexture(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Rocks), new Rectangle(new Point(textureIndex * 64, 0), new Point(64)));
			Body = new WallBody(position, size, new WallColliderCommand(this, new WallImpactParticlesEmitter(this, texture, 15)));
			Renderable = new DummyRenderable();
		}
	}
}