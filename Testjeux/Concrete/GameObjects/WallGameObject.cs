using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class WallGameObject : BaseGameObject
	{
		public WallGameObject(Vector2 position, Vector2 size)
			: base()
		{
			Type = GameObjectType.WALL;
			Body = new WallBody(position, size);
			Renderable = new DummyRenderable();
		}
	}
}