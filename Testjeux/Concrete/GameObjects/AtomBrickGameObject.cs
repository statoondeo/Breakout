using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class AtomBrickGameObject : BaseBrickGameObject
	{
		public AtomBrickGameObject(Vector2 position, float scale, Vector2 center, float radius, float currentAngle, float angleSpeed)
			: base(position, 32, 2)
		{
			Movable = new RotationMovable(this, center, radius, currentAngle, angleSpeed);
			Renderable = new AtomAnimatedTextureRenderable(this, scale);
		}
	}
}