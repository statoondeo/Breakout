using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class SmallButtonGameObject : BaseButtonGameObject
	{
		public SmallButtonGameObject(Vector2 position, string text, Color textColor, ICommand command)
			: base(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Button2), position, text, textColor, command)
		{
		}
	}
}