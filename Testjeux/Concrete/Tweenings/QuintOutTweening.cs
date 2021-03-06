using System;

namespace GameNameSpace
{
	public sealed class QuintOutTweening : BaseTweening
	{
		public QuintOutTweening() : base() { }

		public override float GetStep(float progress)
		{
			return (1.0f - (float)Math.Pow(1 - progress, 5));
		}
	}
}
