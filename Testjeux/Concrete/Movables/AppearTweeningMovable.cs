using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class AppearTweeningMovable : BaseMovable
	{
		protected Vector2 Origin;
		protected Vector2 Destination;
		protected Vector2 DestinationOffset;
		protected float Ttl;
		protected float CurrentTtl;
		protected float Delay;
		protected ITweening Tweening;
		protected bool Started;
		protected bool Ended;

		public AppearTweeningMovable(IGameObject gameObject, ITweening tweening, Vector2 origin, Vector2 destination, float ttl, float delay) 
			: base(gameObject)
		{
			Origin = origin;
			Destination = destination;
			DestinationOffset = Destination - Origin;
			Ttl = ttl;
			CurrentTtl = 0;
			Delay = delay;
			Tweening = tweening;
			Started = Ended = false;
		}

		public override void Move(GameTime gameTime)
		{
			base.Move(gameTime);
			if (!Started)
			{
				Delay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				Started = Delay < 0;
			}
			if (Started && !Ended)
			{
				CurrentTtl += (float)gameTime.ElapsedGameTime.TotalSeconds;
				GameObject.Body.MoveTo(Origin + DestinationOffset * Tweening.GetStep(CurrentTtl / Ttl));
				Ended = CurrentTtl > Ttl;
				if (Ended)
				{
					GameObject.Body.MoveTo(Destination);
				}
			}
		}
	}
}