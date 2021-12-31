using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrainMiniatureGameObject : BaseGameObject
	{
		public BrainMiniatureGameObject(Vector2 position)
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new BrainIdleRenderable(this, 0.5f, Vector2.Zero)
			{
				Layer = 0.81f
			};
			Body.MoveTo(position);
		}
	}
}