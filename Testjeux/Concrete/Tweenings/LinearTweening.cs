namespace GameNameSpace
{
	public sealed class LinearTweening : BaseTweening
	{
		public LinearTweening() : base() { }

		public override float GetStep(float progress) => progress;
	}
}