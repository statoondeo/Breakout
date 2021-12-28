using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class BrainAttackState : BrainState
	{
		private readonly IGameObject Halo1;
		private readonly IGameObject Halo2;

		public BrainAttackState(IStateContainer container, Vector2 offset)
			: base(container, new BrainAttackRenderable(container as IGameObject, 1.0f, offset))
		{
			Halo1 = Services.Instance.Get<ISceneService>().RegisterGameObject(new HaloGameObject(Color.Red, 0.2f, 3.0f));
			Halo1.Renderable.Alpha = 0.25f;

			Halo2 = Services.Instance.Get<ISceneService>().RegisterGameObject(new HaloGameObject(Color.Red, -0.3f, 2.0f));
			Halo2.Renderable.Alpha = 0.4f;
			Halo2.Body.MoveTo((container as IGameObject).Body.Position);

			Halo1.Status = Halo2.Status = GameObjectStatus.IDLE;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			Halo1.Body.MoveTo((Container as IGameObject).Body.Position + new Vector2(128));
			Halo2.Body.MoveTo((Container as IGameObject).Body.Position + new Vector2(128));
		}

		public override bool Damage() => true;

		public override void Enter()
		{
			base.Enter();
			Halo1.Status = Halo2.Status = GameObjectStatus.ACTIVE;
		}

		public override void Exit()
		{
			base.Exit();
			Halo1.Status = Halo2.Status = GameObjectStatus.IDLE;
		}
	}
}