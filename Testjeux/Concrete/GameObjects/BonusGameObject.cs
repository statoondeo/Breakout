using Microsoft.Xna.Framework;

namespace GameNameSpace
{

	public sealed class BonusGameObject : BaseBrickGameObject
	{
		private readonly IGameObject Halo;

		public BonusGameObject(Vector2 position)
			: base(position, 32, 1)
		{
			Body = new BrickBody(position, 32, new BonusColliderCommand(this));
			Renderable = new BonusRenderable(this, 1.0f);
			Halo = new HaloGameObject(Color.White, 0.2f, 0.75f);
			Halo.Renderable.Alpha = 0.4f;
			Vector2 origin = new Vector2(position.X, -500);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(Halo, origin, position));
		}

		public override void Damage()
		{
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			Halo.Body.MoveTo(Body.Position + new Vector2(24));
		}
	}
}