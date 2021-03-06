using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RectFrameRenderable : BaseRenderable
	{
		protected IBoxBody Body;

		public RectFrameRenderable(IBoxBody body)
			: base(Vector2.Zero, 1.0f)
		{
			Body = body;
			Layer = 1.0f;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Services.Instance.Get<IShapeService>().DrawRectangle(Color.YellowGreen, Body.Position.ToPoint(), Body.Size.ToPoint(), spriteBatch);
		}
	}
}