using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CursorGameObject : BaseGameObject
	{
		protected ParticlesEmitterGameObject ParticlesEmitterGameObject;

		public CursorGameObject()
		{
			Type = GameObjectType.CURSOR;
			Movable = new MouseMovable(this);
			Body = new BoxBody(Vector2.Zero, Vector2.One, Vector2.Zero, 0.0f, 1.0f, false, new DummyColliderCommand());
			ParticlesEmitterGameObject = new ParticlesEmitterGameObject(this);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			ParticlesEmitterGameObject.Update(gameTime);
		}
	}
}