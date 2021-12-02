using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrickGameObject : BaseGameObject
	{
		public BrickGameObject(Vector2 position, Point size)
			: base()
		{
			Type = GameObjectType.BRICK;
			Movable = new ImmobileMovable(position, size);
			Collidable = new BrickCollidable(Movable, new BrickCollidableCommand(this));
			Renderable = new TextureRenderable(Movable, ServiceLocator.Instance.Get<ShapeFactory>().CreateTexture(size, Color.Gray));
		}
	}
}