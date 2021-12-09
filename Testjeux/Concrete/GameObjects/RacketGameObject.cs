using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RacketGameObject : BaseGameObject
	{
		public Vector2 Size { get; protected set; }

		public RacketGameObject(Vector2 position, Vector2 size)
			: base()
		{
			Texture2D texture = ServiceLocator.Instance.Get<IShapeService>().CreateTexture(size.ToPoint(), Color.CornflowerBlue);
			Texture2D textureTrail = ServiceLocator.Instance.Get<IShapeService>().CreateTexture(size.ToPoint(), Color.CornflowerBlue);
			Size = size;
			Type = GameObjectType.RACKET;
			Movable = new RacketMouseMovable(this);
			Body = new RacketBody(position, size, new RacketColliderCommand(this, new RacketImpactParticlesEmitter(this, textureTrail, 10)));
			Renderable = new TextureRenderable(this, texture);
		}
	}
}