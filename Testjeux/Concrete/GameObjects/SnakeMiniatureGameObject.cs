using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class SnakeMiniatureGameObject : BaseGameObject
	{
		public SnakeMiniatureGameObject(Vector2 position)
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new SnakeHeadRenderable(this, 1.2f)
			{
				Layer = 0.81f
			};
			Body.MoveTo(position);
		}
	}
}