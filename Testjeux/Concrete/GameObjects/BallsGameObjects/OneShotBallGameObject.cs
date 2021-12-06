using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class OneShotBallGameObject : BallGameObject
	{
		public OneShotBallGameObject(Vector2 position, Vector2 velocity, Vector2 size)
			: base(position, velocity, size)
		{
			Body = new BallBody(position, size, velocity, new OneShotBallColliderCommand(this));
			Renderable = new TextureRenderable(this, ServiceLocator.Instance.Get<AssetManager>().GrayBall);
		}
	}
}