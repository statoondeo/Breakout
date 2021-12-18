using System;

namespace GameNameSpace
{
	public sealed class QuintInTweening : BaseTweening
	{
		public QuintInTweening() : base() { }

		public override float GetStep(float progress)
		{
			return ((float)Math.Pow(progress, 5));
		}
	}
}
