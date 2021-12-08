using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseParticlesEmitter : IParticlesEmitter
	{
		protected IGameObject GameObject;
		protected Texture2D Texture;
		protected int Number;
		protected Vector2 Size;

		protected BaseParticlesEmitter(IGameObject gameObject, Texture2D texture, int number)
		{
			GameObject = gameObject;
			Number = number;
			Texture = texture;
			Size = new Vector2(Texture.Width, Texture.Height);
		}

		public virtual void Emit() { }
		public virtual void Emit(GameTime gameTime) { }
		public virtual void Emit(CollisionTestResult collisionResult) { }
	}
}