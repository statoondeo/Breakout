namespace GameNameSpace
{
	public class BounceOutTweening : BaseTweening
	{
		public BounceOutTweening() : base() { }
		public override float GetStep(float progress)
		{
			float n1 = 7.5625f;
			float d1 = 2.75f;

			if (progress < 1 / d1)
			{
				return (n1 * progress * progress);
			}
			else if (progress < 2 / d1)
			{
				progress -= 1.5f / d1;
				return (n1 * progress * progress + 0.75f);
			}
			else if (progress < 2.5 / d1)
			{
				progress -= 2.25f / d1;
				return (n1 * progress * progress + 0.9375f);
			}
			else
			{
				progress -= 2.625f / d1;
				return (n1 * progress * progress + 0.984375f);
			}
		}
	}
}