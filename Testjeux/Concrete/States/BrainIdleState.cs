using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class BrainIdleState : BrainState
	{
		public BrainIdleState(IStateContainer container, Vector2 offset)
			: base(container, new BrainIdleRenderable(container as IGameObject, 1.0f, offset))
		{
		}

		public override void Enter()
		{
			Vector2 destination = new(522, 88);
			Vector2 origin = new(destination.X, -300);
			IGameObject ShieldGameObject = new ShieldBrickGameObject(Vector2.Zero, 2.1f);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(ShieldGameObject, origin, destination));

			IGameObject trigger = Services.Instance.Get<ISceneService>().RegisterGameObject(new BrainShield1Trigger());
			trigger.Status = GameObjectStatus.ACTIVE;

			destination = new Vector2(168, 208);
			origin = new Vector2(destination.X, -300);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(new GardianWobblerGameObject(destination, 1.0f), origin, destination));

			destination = new Vector2(1048, 208);
			origin = new Vector2(destination.X, -300);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(new GardianWobblerGameObject(destination, 1.0f), origin, destination));
		}
	}
}