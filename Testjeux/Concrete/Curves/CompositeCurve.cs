using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CompositeCurve : ICurve
	{
		protected IList<ICurve> Curves;
		protected int CurrentCurve;
		protected bool Loop;

		public CompositeCurve(bool loop, params ICurve[] curves)
		{
			Loop = loop;
			Curves = new List<ICurve>();
			if ((null != curves) && (0 != curves.Length))
			{
				foreach (ICurve curve in curves)
				{
					Ttl += curve.Ttl;
					Curves.Add(curve);
				}
			}
		}

		public Vector2 Position
		{
			get
			{
				return (Curves[CurrentCurve].Position);
			}
		}

		public float Ttl { get; protected set; }

		public bool Ended => CurrentCurve > (Curves.Count - 1);

		public void Reset()
		{
			CurrentCurve = 0;
			Ttl = 0;
			foreach (ICurve curve in Curves)
			{
				curve.Reset();
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			Curves[CurrentCurve].Update(gameTime);
			if (Curves[CurrentCurve].Ended)
			{
				CurrentCurve++;
				if (Ended && Loop)
				{
					Reset();
				}
			}
		}
	}
}