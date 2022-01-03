using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class ButtonGameObject : BaseButtonGameObject
	{
		public ButtonGameObject(Vector2 position, string text, Color textColor, ICommand command)
			: base(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Button), position, text, textColor, command)
		{
		}
	}
}