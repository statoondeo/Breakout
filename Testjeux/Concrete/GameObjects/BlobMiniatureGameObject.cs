using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BlobMiniatureGameObject : BaseGameObject
	{
		public BlobMiniatureGameObject(Vector2 position)
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new BlobAnimatedTextureRenderable(this, 1.3f)
			{
				Layer = 0.81f
			};
			Body.MoveTo(position);
		}
	}
}