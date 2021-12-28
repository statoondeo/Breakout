using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class LifeBonuseGameObject : BaseGameObject
	{
		public LifeBonuseGameObject()
			: base()
		{
			Type = GameObjectType.NONE;
			Body.Force = new Vector2(0.0f, 4.0f);
			Renderable = new AnimatedTextureRenderable(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.Life), Vector2.Zero, new Point(64), 1.0f, new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 }, 0.1f, true));
		}
	}
	public class LifeMiniatureGameObject : BaseGameObject
	{
		public LifeMiniatureGameObject()
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new AnimatedTextureRenderable(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.Life), Vector2.Zero, new Point(64), 0.5f, new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 }, 0.1f, true))
			{
				Layer = 0.75f
			};
		}
	}
}