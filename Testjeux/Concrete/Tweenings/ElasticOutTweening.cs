using System;

namespace GameNameSpace
{

	public class ElasticOutTweening : BaseTweening
	{
		public ElasticOutTweening() : base() { }

		public override float GetStep(float progress)
		{
			float c4 = (float)(2 * Math.PI) / 3;

			return (float)(progress == 0
				  ? 0
				  : progress == 1
				  ? 1
				  : Math.Pow(2, -10 * progress) * Math.Sin((progress * 10 - 0.75f) * c4) + 1);
		}
	}
}
