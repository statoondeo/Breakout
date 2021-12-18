using System;

namespace GameNameSpace
{

	public sealed class ElasticOutTweening : BaseTweening
	{
		private static readonly float C4 = (float)(2 * Math.PI) / 3;
		public ElasticOutTweening() : base() { }

		public override float GetStep(float progress)
		{
			return (float)(progress == 0
				  ? 0
				  : progress == 1
				  ? 1
				  : Math.Pow(2, -10 * progress) * Math.Sin((progress * 10 - 0.75f) * C4) + 1);
		}
	}
}
