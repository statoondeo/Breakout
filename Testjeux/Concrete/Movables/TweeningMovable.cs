using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class TweeningMovable : BaseMovable
	{
		protected ITweening Tweening { get; set; }
		public float Ttl { get; set; }
		public float CurrentTtl { get; set; }
		public Vector2 Origin { get; set; }
		public Vector2 Destination { get; set; }
		public Vector2 DestinationOffset { get; set; }
		protected bool Ended;

		public TweeningMovable(IGameObject gameObject)
			: base(gameObject)
		{
			Tweening = Services.Instance.Get<ITweeningService>().Get(TweeningName.Linear);
			Ttl = CurrentTtl = 0;
			Origin = Destination = DestinationOffset = Vector2.Zero;
		}

		public void Init(ITweening tweening, float ttl, Vector2 origin, Vector2 destination)
		{
			Tweening = tweening;
			Ttl = ttl;
			CurrentTtl = 0;
			Origin = origin;
			Destination = destination;
			DestinationOffset = Destination - Origin;
			Ended = false;
		}

		public override void Move(GameTime gameTime)
		{
			if (!Ended)
			{
				base.Move(gameTime);
				CurrentTtl += (float)gameTime.ElapsedGameTime.TotalSeconds;
				GameObject.Body.MoveTo(Origin + DestinationOffset * Tweening.GetStep(CurrentTtl / Ttl));
				Ended = CurrentTtl > Ttl;
			}
		}
	}
}