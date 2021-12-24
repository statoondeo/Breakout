namespace GameNameSpace
{
	public class ButtonWrapperColliderCommand : BaseColliderCommand
	{
		protected ICommand Command;

		public ButtonWrapperColliderCommand(IGameObject gameObject, ICommand command)
			: base(gameObject)
		{
			Command = command;
		}

		public override void Execute(IGameObject gameObject, CollisionTestResult collisionResult)
		{
			if (Services.Instance.Get<IInputListenerService>().IsLeftClick())
			{
				Services.Instance.Get<IAssetService>().GetSound(SoundName.Click).Play();
				Command.Execute();
			}
		}
	}
}