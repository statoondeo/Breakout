using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BackgroundGameObject : BaseGameObject
	{
		public BackgroundGameObject(Texture2D texture)
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero)
			{
				Layer = 0.1f
			};
		}
	}
}