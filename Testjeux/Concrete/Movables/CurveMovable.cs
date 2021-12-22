using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CurveMovable : BaseMovable
	{
		protected ICurve Curve;

		public CurveMovable(IGameObject gameObject, ICurve curve)
			: base(gameObject)
		{
			Curve = curve;
		}

		public override void Move(GameTime gameTime)
		{
			Curve.Update(gameTime);
			GameObject.Body.MoveTo(Curve.Position);
		}
	}
}