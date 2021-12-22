using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrainAnimatedTextureRenderable : AnimatedTextureRenderable
	{
		public BrainAnimatedTextureRenderable(IGameObject gameObject, float scale, Vector2 offset)
			: base(
				  gameObject,
				  Services.Instance.Get<IAssetService>().GetTexture(TextureName.Brain),
				  offset,
				  new Point(256),
				  scale,
				  new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, 0.15f, true))
		{ }
	}
}