using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class MouseMovable : BaseMovable
	{
		public MouseMovable(IGameObject gameObject) 
			: base(gameObject) 
		{ }

		public override void Move(GameTime gameTime)
		{
			base.Move(gameTime);
			GameObject.Body.Move(ServiceLocator.Instance.Get<IInputListenerService>().MousePosition().ToVector2() - GameObject.Body.Position);
		}
	}
}