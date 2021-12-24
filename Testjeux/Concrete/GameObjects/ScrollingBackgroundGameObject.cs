using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class ScrollingBackgroundGameObject : BaseGameObject
	{
		public ScrollingBackgroundGameObject(Texture2D texture, Vector2 velocity)
			: base()
		{
			Services.Instance.Get<ISceneService>().RegisterGameObject(new ScrollingRibbonGameObject(texture, velocity));
			IGameObject background = Services.Instance.Get<ISceneService>().RegisterGameObject(new ScrollingRibbonGameObject(texture, velocity));
			background.Body.MoveTo(new Vector2(Services.Instance.Get<IScreenService>().GetScreenSize().X, 0));
			Status = GameObjectStatus.OUTDATED;
		}
	}
}