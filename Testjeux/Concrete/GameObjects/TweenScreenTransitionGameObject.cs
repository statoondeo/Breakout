using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class InScreenTransitionGameObject : TweenScreenTransitionGameObject
	{
		public InScreenTransitionGameObject(ICommand whenEndedCommand)
			: base(Services.Instance.Get<ITweeningService>().Get(TweeningName.Linear), 0.250f, ScreenTransitionDirection.Show, whenEndedCommand)
		{
		}
	}
	public class OutScreenTransitionGameObject : TweenScreenTransitionGameObject
	{
		public OutScreenTransitionGameObject(ICommand whenEndedCommand)
			: base(Services.Instance.Get<ITweeningService>().Get(TweeningName.Linear), 0.250f, ScreenTransitionDirection.Hide, whenEndedCommand)
		{
		}
	}
	public class TweenScreenTransitionGameObject : BaseScreenTransitionGameObject
	{
		protected ITweening Tweening;
		public TweenScreenTransitionGameObject(ITweening tweening, float ttl, ScreenTransitionDirection direction, ICommand whenEndedCommand)
			: base(ttl, direction, whenEndedCommand)
		{
			Tweening = tweening;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			CurrentTtl += DirectionTtl * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (!Ended)
			{
				Renderable.Alpha = Tweening.GetStep(CurrentTtl / MaxTtl);
			}
		}
	}
}
