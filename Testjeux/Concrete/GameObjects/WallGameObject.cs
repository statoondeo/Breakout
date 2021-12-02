using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class WallGameObject : BaseGameObject
	{
		public WallGameObject(Vector2 position, Point size)
			: base()
		{
			Type = GameObjectType.WALL;
			Movable = new ImmobileMovable(position, size);
			Collidable = new WallCollidable(Movable, new WallCollidableCommand(this));
			Renderable = new DummyRenderable();
		}
	}
}