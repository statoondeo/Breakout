using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrainAttackRenderable : AnimatedTextureRenderable
	{
		public BrainAttackRenderable(IGameObject gameObject, float scale, Vector2 offset)
			: base(
				  gameObject,
				  Services.Instance.Get<IAssetService>().GetTexture(TextureName.Brain),
				  offset,
				  new Point(256),
				  scale,
				  new TextureAnimation(new int[] { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 }, 0.1f, true))
		{ }
	}
}