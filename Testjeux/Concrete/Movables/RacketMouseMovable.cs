using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class RacketMouseMovable : BaseMovable
	{
		public RacketMouseMovable(IGameObject gameObject) 
			: base(gameObject) { }

		public override void Move(GameTime gameTime)
		{
			base.Move(gameTime);
			RacketGameObject racket = GameObject as RacketGameObject;
			float newX = Services.Instance.Get<IInputListenerService>().MousePosition().X - racket.Size.X / 2;
			GameObject.Body.MoveTo(new Vector2(MathHelper.Clamp(newX, 0, Services.Instance.Get<IScreenService>().GetScreenSize().X - racket.Size.X), GameObject.Body.Position.Y));
		}
	}
}