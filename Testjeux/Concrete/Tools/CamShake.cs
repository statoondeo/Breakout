using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CamShake
	{
		protected static readonly float MaxCamShakeTtl = 0.25f;
		protected float CamShakeTtl;
		protected bool CamShakeSpring;
		protected readonly ITweening CamShakeTwwening;
		protected Point mValue;

		public Point Value
		{
			get => mValue;
			set
			{
				CamShakeTtl = MaxCamShakeTtl;
				CamShakeSpring = true;
				mValue = value;
			}
		}

		public CamShake()
		{
			CamShakeSpring = false;
			CamShakeTwwening = Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut);
		}

		public void Update(GameTime gameTime)
		{
			// Amortissement du camshake
			if (CamShakeSpring)
			{
				CamShakeTtl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (CamShakeTtl < 0)
				{
					CamShakeSpring = false;
					mValue = Point.Zero;
				}
				else
				{
					float ttlRatio = CamShakeTwwening.GetStep(CamShakeTtl / MaxCamShakeTtl);
					mValue = new Point((int)(mValue.X * ttlRatio), (int)(mValue.Y * ttlRatio));
				}
			}
		}
	}
}