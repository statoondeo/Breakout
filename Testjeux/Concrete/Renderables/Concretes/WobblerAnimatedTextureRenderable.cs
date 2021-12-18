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
	public class BlobAnimatedTextureRenderable : AnimatedTextureRenderable
	{
		public BlobAnimatedTextureRenderable(IGameObject gameObject, float scale)
			: base(
				  gameObject,
				  Services.Instance.Get<IAssetService>().GetTexture(TextureName.Blob),
				  Vector2.Zero,
				  new Point(64),
				  scale,
				  new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 }, (Services.Instance.Get<IRandomService>().Next() + 1) * 0.05f, true))
		{
			Animation.Start(Services.Instance.Get<IRandomService>().Next(0, 31));
		}
	}
	public class AtomAnimatedTextureRenderable : AnimatedTextureRenderable
	{
		public AtomAnimatedTextureRenderable(IGameObject gameObject, float scale)
			: base(
				  gameObject,
				  Services.Instance.Get<IAssetService>().GetTexture(TextureName.Atom),
				  Vector2.Zero,
				  new Point(64),
				  scale,
				  new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, 0.05f, true))
		{ }
	}
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