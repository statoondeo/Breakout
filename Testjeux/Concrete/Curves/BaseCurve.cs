using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public abstract class BaseCurve : ICurve
	{
		protected IList<Vector2> Points;
		protected float CurrentTtl;
		protected ITweening Interpolation;

		protected BaseCurve(float ttl, params Vector2[] points) 
		{
			Ttl = ttl;
			Points = new List<Vector2>();
			if ((null != points) && (0 != points.Length))
			{
				foreach (Vector2 point in points)
				{
					Points.Add(point);
				}
			}
			Interpolation = Services.Instance.Get<ITweeningService>().Get(TweeningName.Linear);
			Reset();
		}

		public bool Ended { get; protected set; }

		public float Ttl { get; protected set; }

		public virtual Vector2 Position => Vector2.Zero;

		public void Reset()
		{
			CurrentTtl = 0;
			Ended = false;
		}

		public virtual void Update(GameTime gameTime)
		{
			CurrentTtl += (float)gameTime.ElapsedGameTime.TotalSeconds;
			Ended = CurrentTtl > Ttl;
		}
	}
}