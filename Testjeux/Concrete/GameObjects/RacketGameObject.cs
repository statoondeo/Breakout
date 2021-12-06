using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class RacketGameObject : BaseGameObject
	{
		public Vector2 Size { get; protected set; }
		public RacketGameObject(Vector2 position, Vector2 size)
			: base()
		{
			Size = size;
			Type = GameObjectType.RACKET;
			Movable = new RacketMouseMovable(this);
			Body = new RacketBody(position, size);
			Renderable = new TextureRenderable(this, ServiceLocator.Instance.Get<ShapeFactory>().CreateTexture(size.ToPoint(), Color.CornflowerBlue));
		}
	}
}