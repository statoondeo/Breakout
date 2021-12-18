namespace GameNameSpace
{
	public abstract class BaseTweening : ITweening
	{
		protected BaseTweening() { }

		public abstract float GetStep(float progress);
	}
}