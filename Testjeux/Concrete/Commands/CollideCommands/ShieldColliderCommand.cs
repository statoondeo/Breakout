using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class ShieldColliderCommand : BaseColliderCommand
	{
		protected ShieldBrickGameObject ShieldGameObject;
		public ShieldColliderCommand(IGameObject gameObject)
			: base(gameObject)
		{
			ShieldGameObject = gameObject as ShieldBrickGameObject;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			base.Execute(gameObject, collisionResult);

			// Vibration lors du rebond
			Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new ShieldTextureRenderable(null, 1.0f, Vector2.Zero), GameObject.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * GameObject.Renderable.Scale * 0.25f, ShieldBrickGameObject.TextureSize, 0.25f, 1.9f));
			Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new ShieldTextureRenderable(null, 1.0f, Vector2.Zero), GameObject.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * GameObject.Renderable.Scale * 0.25f, ShieldBrickGameObject.TextureSize, 0.5f, 2.1f));
			Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new ShieldTextureRenderable(null, 1.0f, Vector2.Zero), GameObject.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * GameObject.Renderable.Scale * 0.25f, ShieldBrickGameObject.TextureSize, 0.75f, 2.3f));

			if (gameObject.Type == GameObjectType.BALL)
			{
				IBallGameObject ball = gameObject as IBallGameObject;
				Vector2 velocity = Vector2.Normalize(ball.Body.Velocity);
				ball.Body.Velocity = velocity * ball.Speed;
				(GameObject as IBrickGameObject).Damage();
			}
		}
	}
}