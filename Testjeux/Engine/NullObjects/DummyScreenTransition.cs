namespace GameNameSpace
{
	public sealed class DummyScreenTransition : BaseScreenTransitionGameObject
	{
		public DummyScreenTransition(ICommand command)
			: base(0.0f, ScreenTransitionDirection.Show, command)
		{
		}
	}
}
