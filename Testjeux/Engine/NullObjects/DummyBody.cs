using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class DummyBody : BaseBody
	{
		public DummyBody()
			: base(Vector2.Zero, Vector2.Zero, 0.0f, 0.0f, true, new DummyColliderCommand())
		{
		}

		public override void Draw(SpriteBatch spriteBatch) { }
	}
}
