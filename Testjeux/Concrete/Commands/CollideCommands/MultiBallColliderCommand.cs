using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class MultiBallColliderCommand : BallColliderCommand
	{
		public MultiBallColliderCommand(IGameObject gameObject, IParticlesEmitter particlesEmitterGameObject)
			: base(gameObject, particlesEmitterGameObject)
		{
			ParticlesEmitterGameObject = particlesEmitterGameObject;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			if (gameObject is RacketGameObject)
			{
				IList<IGameObject> balls = Services.Instance.Get<ISceneService>().GetObjects(item => item is MultiBallGameObject);
				if (balls.Count < 5)
				{
					IGameObject newBall = Services.Instance.Get<ISceneService>().RegisterGameObject(new MultiBallGameObject(new SingleBallGameObject(GameObject.Body.Position, 700, new Vector2(32))));
					newBall.Body.Velocity = new Vector2(-GameObject.Body.Velocity.X, GameObject.Body.Velocity.Y - collisionResult.Depth);
				}
				else
				{
					IGameObject innerBall = null;
					foreach (IGameObject ball in balls)
					{
						MultiBallGameObject multiBall = ball as MultiBallGameObject;
						multiBall.Status = GameObjectStatus.OUTDATED;
						innerBall = multiBall.DecoratedGameObject;
						innerBall.Body.MoveTo(multiBall.Body.Position);
						innerBall.Body.Velocity = multiBall.Body.Velocity;
						Services.Instance.Get<ISceneService>().RegisterGameObject(innerBall);
					}
				}
			}
		}
	}
}