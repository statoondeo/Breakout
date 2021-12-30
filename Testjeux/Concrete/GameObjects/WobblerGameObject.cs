using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class WobblerGameObject : BaseBrickGameObject
	{
		public WobblerGameObject(Vector2 position, float scale)
			: base(position, 32, 1)
		{
			Renderable = new WobblerRenderable(this, scale);
		}
	}
}