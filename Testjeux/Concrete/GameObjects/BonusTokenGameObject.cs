using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class BonusTokenGameObject : BaseBrickGameObject
	{
		public BonusTokenGameObject(Vector2 position)
			: base(position, 32, 1)
		{
			Body = new BrickBody(position, 32, new DummyColliderCommand());
			Renderable = new BonusRenderable(this, 1.0f);
		}
	}
}