using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BallGameObject : BaseGameObject
	{
		protected IParticlesEmitter TrailParticlesEmitter;
		protected IParticlesEmitter BallExplosionParticlesEmitter;
		public bool Exploded { get; set; }

		public float Speed { get; protected set; }

		public BallGameObject(Vector2 position, float speed, Vector2 size)
			: base()
		{
			Speed = speed;
			Type = GameObjectType.BALL;
			Body = new BallBody(position, size, Vector2.Zero, new BallColliderCommand(this));
			Movable = new VelocityMovable(this);
			Renderable = new TextureRenderable(this, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedBall));
			TrailParticlesEmitter = new BallTrailParticlesEmitter(this, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedBall), 5);
			BallExplosionParticlesEmitter = new BallExplosionParticlesEmitter(this, ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.RedBall), 25);
			Exploded = false;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			TrailParticlesEmitter.Emit(gameTime);
			if (Exploded)
			{
				BallExplosionParticlesEmitter.Emit();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}