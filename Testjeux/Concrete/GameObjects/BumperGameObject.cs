using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public abstract class BumperGameObject : BaseGameObject
	{
		protected BumperGameObject(Vector2 position, float radius)
			: base()
		{
			Type = GameObjectType.BRICK;
			Body = new BumperBody(position, radius, new BumperColliderCommand(this, new DummyParticlesEmitter()));
		}
	}
}