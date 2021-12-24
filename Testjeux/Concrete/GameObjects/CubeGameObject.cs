using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class CubeGameObject : BaseBrickGameObject
	{
		public CubeGameObject(Vector2 position)
			: base(position, 32, 1)
		{
			Renderable = new CubeRenderable(this, 1.0f);
		}

		public override void Damage()
		{
			//base.Damage();
		}
	}
}