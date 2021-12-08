using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BallGameObject : BaseGameObject
	{
		protected IParticlesEmitter TrailParticlesEmitter;

		public BallGameObject(Vector2 position, Vector2 velocity, Vector2 size)
			: base()
		{
			Type = GameObjectType.BALL;
			Body = new BallBody(position, size, velocity, new BallColliderCommand(this));
			Movable = new VelocityMovable(this);
			Renderable = new TextureRenderable(this, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedBall));
			TrailParticlesEmitter = new BallTrailParticlesEmitter(this, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedBall), 5);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			TrailParticlesEmitter.Emit(gameTime);
		}
	}
}