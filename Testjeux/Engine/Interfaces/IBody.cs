using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace GameNameSpace
{
	public interface IBody
	{
		Vector2 Force { get; set; }
		Vector2 Position { get; }
		Vector2 Velocity { get; set; }
		float Mass { get; }
		float InvMass { get; }
		float Restitution { get; }
		bool IsStatic { get; }
		IColliderCommand CollideCommand { get; }
		void Move(Vector2 offset);
		void MoveTo(Vector2 newPosition);
		void Draw(SpriteBatch spriteBatch);
	}
}
