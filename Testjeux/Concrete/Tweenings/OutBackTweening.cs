using System;

namespace GameNameSpace
{
	public sealed class OutBackTweening : BaseTweening
	{
		private static readonly float C1 = 1.70158f;
		private static readonly float C2 = C1 + 1;

		public OutBackTweening() : base() { }

		public override float GetStep(float progress)
		{
			return ((float)(1 + C2 * Math.Pow(progress - 1, 3) + C1 * Math.Pow(progress - 1, 2)));
		}
	}
}