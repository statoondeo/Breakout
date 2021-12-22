using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class ShieldTextureRenderable : TextureRenderable
	{
		public static ShieldTextureRenderable Create(IGameObject gameObject, float scale, Vector2 offset)
		{
			return (new ShieldTextureRenderable(gameObject, scale, offset));
		}

		public ShieldTextureRenderable(IGameObject gameObject, float scale, Vector2 offset)
			: base(gameObject, Services.Instance.Get<IAssetService>().GetTexture(TextureName.Shield), scale, offset)
		{ }
	}
}