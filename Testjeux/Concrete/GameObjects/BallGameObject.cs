using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BallGameObject : BaseGameObject
	{
		public BallGameObject(Vector2 position, Vector2 velocity, Vector2 size)
			: base()
		{
			Type = GameObjectType.BALL;
			Body = new BallBody(position, size, velocity, new BallColliderCommand(this));
			Movable = new VelocityMovable(this);
			Renderable = new TextureRenderable(this, ServiceLocator.Instance.Get<AssetManager>().RedBall);
		}
	}
}