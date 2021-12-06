using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class PhantomGeneratorBallColliderCommand : BallColliderCommand
	{
		protected int Speed;

		public PhantomGeneratorBallColliderCommand(IGameObject gameObject) 
			: base(gameObject) 
		{
			Speed = 200;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);
			if (gameObject.Type == GameObjectType.BRICK)
			{
				float newAngle = (float)(5 * MathHelper.Pi / 4 + MathHelper.Pi / 2 * (new Random()).NextDouble());
				ServiceLocator.Instance.Get<GameState>().CurrentScene.RegisterGameObject(new OneShotBallGameObject(GameObject.Body.Position, new Vector2((float)Math.Cos(newAngle) * Speed, (float)Math.Sin(newAngle) * Speed), new Vector2(24)));
			}
		}
	}
}