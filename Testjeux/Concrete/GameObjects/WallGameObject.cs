using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class WallGameObject : BaseGameObject
	{
		public WallGameObject(Vector2 position, Vector2 size)
			: base()
		{
			Type = GameObjectType.WALL;
			Body = new WallBody(position, size, new WallColliderCommand(this, new WallImpactParticlesEmitter(this, ServiceLocator.Instance.Get<IShapeService>().CreateTexture(new Point(15), Color.BlueViolet), 15)));
			Renderable = new DummyRenderable();
		}
	}
}