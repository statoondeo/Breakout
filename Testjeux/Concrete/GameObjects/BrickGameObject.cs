using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BrickGameObject : BaseGameObject
	{
		public int Health { get; set; }

		public BrickGameObject(Vector2 position, Vector2 size, int health)
			: base()
		{
			Texture2D texture = ServiceLocator.Instance.Get<IShapeService>().CreateTexture(size.ToPoint(), Color.Gray);
			Type = GameObjectType.BRICK;
			Body = new BrickBody(position, size, new BrickColliderCommand(this, new BrickExplosionParticlesEmitter(this, texture, 10)));
			Renderable = new TextureRenderable(this, texture);
			Health = health;
		}
	}
}