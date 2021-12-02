using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class RacketGameObject : BaseGameObject
	{
		public RacketGameObject(Vector2 position, Point size)
			: base()
		{
			Type = GameObjectType.RACKET;
			Movable = new MouseMovable(position, size);
			Collidable = new RacketCollidable(Movable, new RacketCollidableCommand(this));
			Renderable = new TextureRenderable(Movable, ServiceLocator.Instance.Get<ShapeFactory>().CreateTexture(size, Color.CornflowerBlue));
		}
	}
}