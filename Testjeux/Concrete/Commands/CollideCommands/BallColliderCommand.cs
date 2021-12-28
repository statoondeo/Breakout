using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BallColliderCommand : BaseColliderCommand
	{
		protected IParticlesEmitter ParticlesEmitterGameObject;

		public BallColliderCommand(IGameObject gameObject, IParticlesEmitter particlesEmitterGameObject)
			: base(gameObject)
		{
			ParticlesEmitterGameObject = particlesEmitterGameObject;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			ParticlesEmitterGameObject.Emit(collisionResult);
			Services.Instance.Get<IAssetService>().GetSound(SoundName.Collision).Play();
			Vector2 velocity = Vector2.Normalize(GameObject.Body.Velocity);
			float speed = Vector2.Dot(velocity, GameObject.Body.Velocity);
			Services.Instance.Get<ISceneService>().CamShake.Value = (velocity * speed / 50.0f).ToPoint();
		}
	}
}