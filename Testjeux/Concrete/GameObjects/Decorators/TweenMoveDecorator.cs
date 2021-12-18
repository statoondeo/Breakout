using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class TweenMoveDecorator : BaseGameObjectDecorator
	{
		protected Vector2 Origin;
		protected Vector2 Destination;
		protected Vector2 DestinationOffset;
		protected float Ttl;
		protected float CurrentTtl;
		protected float Delay;
		protected IBody InnerBody;
		protected ITweening Tweening;
		protected bool Started;
		protected bool Ended;

		public TweenMoveDecorator(IGameObject gameObject, ITweening tweening, Vector2 origin, Vector2 destination, float ttl, float delay) 
			: base(gameObject)
		{
			Origin = origin;
			Destination = destination;
			DestinationOffset = Destination - Origin;
			Ttl = ttl;
			CurrentTtl = 0;
			Delay = delay;
			InnerBody = new InvisibleBody(origin);
			Tweening = tweening;
			Started = Ended = false;
			Status = GameObjectStatus.ACTIVE;
		}

		//public override IBody Body => InnerBody;
		public override GameObjectStatus Status { get; set; }

		public override void Update(GameTime gameTime)
		{
			DecoratedGameObject.Renderable.Update(gameTime);
			if (!Started)
			{
				DecoratedGameObject.Body.MoveTo(Origin);
				Delay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				Started = Delay < 0;
			}
			if (Started && !Ended)
			{
				CurrentTtl += (float)gameTime.ElapsedGameTime.TotalSeconds;
				DecoratedGameObject.Body.MoveTo(Origin + DestinationOffset * Tweening.GetStep(CurrentTtl / Ttl));
				Ended = CurrentTtl > Ttl;
				if (Ended)
				{
					DecoratedGameObject.Body.MoveTo(Destination);
					Status = GameObjectStatus.OUTDATED;
					Services.Instance.Get<ISceneService>().RegisterGameObject(DecoratedGameObject);
				}
			}
		}
	}
}