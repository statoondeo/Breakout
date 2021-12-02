using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BallGameObject : BaseGameObject
	{
		public BallGameObject(Vector2 position, Vector2 velocity, Point size)
			: base()
		{
			Type = GameObjectType.BALL;
			Movable = new VelocityMovable(position, size, velocity);
			Collidable = new BallCollidable(Movable, new BallCollidableCommand(Movable as VelocityMovable));
			Renderable = new TextureRenderable(Movable, ServiceLocator.Instance.Get<AssetManager>().RedBall);
		}
	}
}