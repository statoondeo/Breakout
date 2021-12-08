namespace GameNameSpace
{
	public abstract class BaseTweening : ITweening
	{
		protected BaseTweening() { }

		public virtual float GetStep(float progress) => progress;
	}
}