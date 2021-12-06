using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrickGameObject : BaseGameObject
	{
		public BrickGameObject(Vector2 position, Vector2 size)
			: base()
		{
			Type = GameObjectType.BRICK;
			Body = new BrickBody(position, size, new BrickColliderCommand(this));
			Renderable = new TextureRenderable(this, ServiceLocator.Instance.Get<ShapeFactory>().CreateTexture(size.ToPoint(), Color.Gray));
		}
	}
}