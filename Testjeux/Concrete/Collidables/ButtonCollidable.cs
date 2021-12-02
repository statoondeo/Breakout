using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class ButtonCollidable : RectangleCollidable
	{
		protected ICommand Command;
		protected bool CommandExecuted;

		public ButtonCollidable(IPositionable positionable, ICommand command) 
			: base(positionable) 
		{
			Command = command;
			CommandExecuted = false;
		}

		public override void Collide()
		{
			base.Collide();
			InputListener input = ServiceLocator.Instance.Get<InputListener>();
			if (!CommandExecuted && input.IsLeftClick() && Collider.IsCollision(this, new RectangleCollidable(new DefaultPositionable(input.MousePosition().ToVector2(), Point.Zero))))
			{
				Command.Execute();
				CommandExecuted = true;
			}
		}
	}
}