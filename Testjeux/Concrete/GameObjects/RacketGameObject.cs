using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class RacketGameObject : BaseGameObject
	{
		public static readonly Point DEFAULT_SIZE = new Point(120, 20);

		public RacketGameObject(Vector2 position)
			: base()
		{
			Type = GameObjectType.RACKET;
			Movable = new MouseMovable(position, DEFAULT_SIZE);
			Collidable = new RacketCollidable(Movable, new RacketCollidableCommand(this));
			Renderable = new TextureRenderable(Movable, ServiceLocator.Instance.Get<ShapeFactory>().CreateTexture(DEFAULT_SIZE, Color.CornflowerBlue));
		}
	}
}