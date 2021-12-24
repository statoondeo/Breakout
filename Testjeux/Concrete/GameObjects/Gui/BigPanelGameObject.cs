using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BigPanelGameObject : BaseGameObject
	{

		public BigPanelGameObject()
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new TextureRenderable(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.BigPanel), 1.0f, Vector2.Zero)
			{
				Layer = 0.6f
			};
		}
	}
}