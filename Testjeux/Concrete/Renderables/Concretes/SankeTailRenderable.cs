using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class SankeTailRenderable : AnimatedTextureRenderable
	{
		public SankeTailRenderable(IGameObject gameObject)
			: base(
				  gameObject,
				  Services.Instance.Get<IAssetService>().GetTexture(TextureName.SerpentRear),
				  new Vector2(32.0f),
				  new Point(64),
				  1.0f,
				  new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, 0.05f, true))
		{ }
	}
}