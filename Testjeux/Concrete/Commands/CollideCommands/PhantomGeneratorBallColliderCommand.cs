//using System;
//using System.Linq;
//using Microsoft.Xna.Framework;

//namespace GameNameSpace
//{
//	public class PhantomGeneratorBallColliderCommand : BallColliderCommand
//	{
//		protected Vector2 Speed;

//		public PhantomGeneratorBallColliderCommand(IGameObject gameObject) 
//			: base(gameObject) 
//		{
//			Speed = new Vector2(200, -700);
//		}

//		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
//		{
//			base.Execute(gameObject, collisionResult);
//			if (gameObject.Type == GameObjectType.BRICK)
//			{
//				//// Récupération de la raquette
//				//RacketGameObject racket = ServiceLocator.Instance.Get<ISceneService>().GetCurrent().GameObjectsCollection.First(item => item is RacketGameObject) as RacketGameObject;
//				//float newAngle = (float)(5 * MathHelper.Pi / 4 + MathHelper.Pi / 2 * (new Random()).NextDouble());
//				//ServiceLocator.Instance.Get<ISceneService>().GetCurrent().RegisterGameObject(new OneShotBallGameObject(racket.Body.Position, new Vector2((float)Math.Cos(newAngle) * Speed.X, (float)Math.Sin(newAngle) * Speed.Y), new Vector2(24)));
//			}
//		}
//	}
//}