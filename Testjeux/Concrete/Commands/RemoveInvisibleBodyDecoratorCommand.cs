namespace GameNameSpace
{
	public class RemoveInvisibleBodyDecoratorCommand : BaseCommand
	{
		protected IGameObject GameObject;

		public RemoveInvisibleBodyDecoratorCommand(IGameObject gameObject)
			: base()
		{
			GameObject = gameObject;
		}

		public override void Execute()
		{
			if (GameObject is InvisibleBodyDecorator)
			{
				GameObject.Status = GameObjectStatus.OUTDATED;
				Services.Instance.Get<ISceneService>().RegisterGameObject((GameObject as InvisibleBodyDecorator).DecoratedGameObject);
			}
		}
	}
}