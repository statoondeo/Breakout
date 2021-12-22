using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class WobblerAnimatedTextureRenderable : AnimatedTextureRenderable
	{
		public WobblerAnimatedTextureRenderable(IGameObject gameObject, float scale)
			: base(
				  gameObject,
				  Services.Instance.Get<IAssetService>().GetTexture(TextureName.Wobbler),
				  Vector2.Zero,
				  new Point(64),
				  scale,
				  new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 0.15f, true))
		{ }
	}
}