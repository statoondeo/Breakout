using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BallGameObject : BaseGameObject
	{
		public static readonly Point DEFAULT_SIZE = new Point(10);

		public BallGameObject(Vector2 position, Vector2 velocity)
			: base()
		{
			Type = GameObjectType.BALL;
			Movable = new VelocityMovable(position, DEFAULT_SIZE, velocity);
			Collidable = new BallCollidable(Movable, new BallCollidableCommand(Movable as VelocityMovable));
			Renderable = new TextureRenderable(Movable, ServiceLocator.Instance.Get<ShapeFactory>().CreateTexture(DEFAULT_SIZE, Color.OrangeRed));
		}
	}
}