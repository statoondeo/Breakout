using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RotatingBackgroundGameObject : BaseGameObject
	{
		public RotatingBackgroundGameObject(Texture2D texture)
			: base()
		{
			Vector2 centerScreen = ServiceLocator.Instance.Get<IScreenService>().GetScreenSize().ToVector2() * 0.5f;
			Type = GameObjectType.NONE;
			Movable = new RevolutionMovable(this, centerScreen, 0.0f, 0.01f);
			Renderable = new TextureRenderable(this, texture, 1.0f, centerScreen);
		}
	}
}