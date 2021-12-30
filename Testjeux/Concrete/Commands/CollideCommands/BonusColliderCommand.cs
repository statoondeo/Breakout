using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class BonusColliderCommand : BaseColliderCommand
	{
		private readonly IParticlesEmitter ParticlesEmitter;
		private float Ttl;
		private readonly float MaxTtl;

		public BonusColliderCommand(IGameObject gameobject) 
			: base(gameobject)
		{
			MaxTtl = 1.0f;
			ParticlesEmitter = new BrickExplosionParticlesEmitter(GameObject, Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow), 15);
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			ParticlesEmitter.Emit(collisionResult);
			IList<IGameObject> balls = Services.Instance.Get<ISceneService>().GetObjects(item => item is MultiBallGameObject);
			if (Ttl + MaxTtl <= Services.Instance.Get<ISceneService>().TotalGameTime)
			{
				IGameObject newBall = Services.Instance.Get<ISceneService>().RegisterGameObject(new SingleBallGameObject(gameObject.Body.Position + collisionResult.Normal * collisionResult.Depth, 700, new Vector2(32)));
				newBall.Body.Velocity = gameObject.Body.Velocity * collisionResult.Normal;
				Ttl = Services.Instance.Get<ISceneService>().TotalGameTime;
			}
		}
	}
}