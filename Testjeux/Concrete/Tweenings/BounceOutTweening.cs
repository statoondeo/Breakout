namespace GameNameSpace
{
	public sealed class BounceOutTweening : BaseTweening
	{
		private static readonly float N1 = 7.5625f;
		private static readonly float D1 = 2.75f;

		public BounceOutTweening() : base() { }

		public override float GetStep(float progress)
		{
			if (progress < 1 / D1)
			{
				return (N1 * progress * progress);
			}
			else if (progress < 2 / D1)
			{
				progress -= 1.5f / D1;
				return (N1 * progress * progress + 0.75f);
			}
			else if (progress < 2.5 / D1)
			{
				progress -= 2.25f / D1;
				return (N1 * progress * progress + 0.9375f);
			}
			else
			{
				progress -= 2.625f / D1;
				return (N1 * progress * progress + 0.984375f);
			}
		}
	}
}