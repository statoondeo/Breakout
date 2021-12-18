using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IGameObject
	{
		GameObjectStatus Status { get; set; }
		GameObjectType Type { get; }
		IMovable Movable { get; set; }
		IBody Body { get; set; }
		IRenderable Renderable { get; set; }
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch);
	}
}