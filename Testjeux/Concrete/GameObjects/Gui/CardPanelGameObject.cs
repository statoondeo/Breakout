using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CardPanelGameObject : BaseGameObject
	{

		public CardPanelGameObject(Vector2 position)
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new TextureRenderable(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.Card), 1.0f, Vector2.Zero)
			{
				Layer = 0.7f
			};
			Body.MoveTo(position);
		}
	}
}