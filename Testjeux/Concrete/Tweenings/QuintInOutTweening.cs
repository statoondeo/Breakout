using System;

namespace GameNameSpace
{
	public sealed class QuintInOutTweening : BaseTweening
	{
		public QuintInOutTweening() : base() { }

		public override float GetStep(float progress)
		{
			return (progress < 0.5 ? 16 * progress * progress * progress * progress * progress : 1 - (float)Math.Pow(-2 * progress + 2, 5) / 2);
		}
	}
}
