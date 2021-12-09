using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BumperGameObject : BaseGameObject
	{
		public BumperGameObject(Vector2 position)
			: base()
		{
			Texture2D texture = ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.PurpleBall);
			Type = GameObjectType.BRICK;
			Body = new BumperBody(position, texture.Width / 2);
			Renderable = new TextureRenderable(this, texture);
		}
	}
}