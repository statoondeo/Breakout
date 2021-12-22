using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BezierCurve : BaseCurve 
	{
		public BezierCurve(float ttl, params Vector2[] points)
			: base(ttl, points)
		{
		}

		public override Vector2 Position
		{
			get
			{
				float progress = CurrentTtl / Ttl;
				IList<Vector2> tmpList = Points;
				IList<Vector2> resultList = new List<Vector2>();

				while (tmpList.Count > 1)
				{
					for (int i = 0; i < (tmpList.Count - 1); i++)
					{
						Vector2 point = tmpList[i];
						Vector2 Nextpoint = tmpList[i + 1];
						resultList.Add(new Vector2(point.X + (Nextpoint.X - point.X) * Interpolation.GetStep(progress), point.Y + (Nextpoint.Y - point.Y) * Interpolation.GetStep(progress)));
					}
					tmpList = resultList;
					resultList = new List<Vector2>();
				}

				return (tmpList.Count == 1 ? tmpList[0] : Vector2.Zero);
			}
		}
	}
}