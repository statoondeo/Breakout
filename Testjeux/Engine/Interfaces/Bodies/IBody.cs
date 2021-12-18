using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IBody
	{
		Vector2 Force { get; set; }
		Vector2 Position { get; }
		Vector2 Velocity { get; set; }
		Vector2 RotationOrigin { get; set; }
		float Angle { get; set; }
		float Restitution { get; }
		bool IsStatic { get; set; }
		IColliderCommand CollideCommand { get; set; }
		void Move(Vector2 offset);
		void MoveTo(Vector2 newPosition);
		void Draw(SpriteBatch spriteBatch);
	}
}
