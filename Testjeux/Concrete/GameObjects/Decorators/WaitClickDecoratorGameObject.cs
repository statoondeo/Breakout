using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class WaitClickDecoratorGameObject : BaseGameObjectDecorator
	{
		protected ICommand CommandWhenReleased;

		public WaitClickDecoratorGameObject(IGameObject gameObject, ICommand commandWhenReleased) : base(gameObject) 
		{
			Status = GameObjectStatus.ACTIVE;
			CommandWhenReleased = commandWhenReleased;
		}

		public override void Update(GameTime gameTime)
		{
			DecoratedGameObject.Renderable.Update(gameTime);
			if (Services.Instance.Get<IInputListenerService>().IsLeftClick())
			{
				Status = GameObjectStatus.OUTDATED;
				CommandWhenReleased.Execute();
				Services.Instance.Get<ISceneService>().RegisterGameObject(DecoratedGameObject);
			}
		}
		public override GameObjectStatus Status { get; set; }
	}
}