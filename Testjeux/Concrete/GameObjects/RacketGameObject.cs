using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class RacketGameObject : BaseGameObject
	{
		private readonly IParticlesEmitter LeftTrailParticlesEmitter;
		private readonly IParticlesEmitter RightTrailParticlesEmitter;
		private readonly IGameObject HaloRenderable;
		private readonly IGameObject Reactor1Renderable;
		private readonly IGameObject Reactor2Renderable;

		public Vector2 Size { get; private set; }

		public RacketGameObject(Vector2 position)
			: base()
		{
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.Platform);
			Size = new Vector2(texture.Width, texture.Height);
			Type = GameObjectType.RACKET;
			Movable = new RacketMouseMovable(this);
			Body = new RacketBody(position, Size, new RacketColliderCommand(this, new DummyParticlesEmitter()));
			Renderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero);

			LeftTrailParticlesEmitter = new RacketTrailParticlesEmitter(this, 3 * (float)Math.PI / 4, new Vector2(-8, 46));
			RightTrailParticlesEmitter = new RacketTrailParticlesEmitter(this, (float)Math.PI / 4, new Vector2(128, 46));

			HaloRenderable = Services.Instance.Get<ISceneService>().RegisterGameObject(new HaloGameObject(Color.White, 0.1f));
			HaloRenderable.Renderable.Alpha = 0.1f;

			Reactor1Renderable = Services.Instance.Get<ISceneService>().RegisterGameObject(new HaloGameObject(Color.OrangeRed, 0.2f, 0.5f));
			Reactor1Renderable.Renderable.Alpha = 0.25f;

			Reactor2Renderable = Services.Instance.Get<ISceneService>().RegisterGameObject(new HaloGameObject(Color.OrangeRed, -0.2f, 0.5f));
			Reactor2Renderable.Renderable.Alpha = 0.25f;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			HaloRenderable.Body.MoveTo(Body.Position + Size * 0.5f);
			Reactor1Renderable.Body.MoveTo(Body.Position + new Vector2(10, 26));
			Reactor2Renderable.Body.MoveTo(Body.Position + new Vector2(110, 26));
			LeftTrailParticlesEmitter.Emit(gameTime);
			RightTrailParticlesEmitter.Emit(gameTime);
		}
	}
}