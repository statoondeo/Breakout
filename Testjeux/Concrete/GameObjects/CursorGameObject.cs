using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class CursorGameObject : BaseGameObject
	{
		private readonly IGameObject Halo;

		public CursorGameObject()
		{
			Type = GameObjectType.CURSOR;
			Movable = new MouseMovable(this);
			Body = new BoxBody(Vector2.Zero, Vector2.One, Vector2.Zero, 1.0f, false, new DummyColliderCommand());

			Halo = Services.Instance.Get<ISceneService>().RegisterGameObject(new HaloGameObject(Color.White, 1.0f, 0.5f));
			Halo.Renderable.Alpha = 0.15f;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			Halo.Body.MoveTo(Body.Position);
		}
	}
}