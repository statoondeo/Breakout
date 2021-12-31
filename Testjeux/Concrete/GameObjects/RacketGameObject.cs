using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class RacketGameObject : BaseGameObject
	{
		private readonly IParticlesEmitter LeftTrailParticlesEmitter;
		private readonly IParticlesEmitter RightTrailParticlesEmitter;
		private readonly IGameObject HaloAdorner;
		private readonly IGameObject Reactor1Adorner;
		private readonly IGameObject Reactor2Adorner;

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

			Vector2 adornerPosition = Body.Position + Size * 0.5f;
			HaloAdorner = new HaloGameObject(Color.White, 0.1f);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(HaloAdorner, new Vector2(adornerPosition.X, -300), adornerPosition));
			HaloAdorner.Renderable.Alpha = 0.1f;

			adornerPosition = Body.Position + new Vector2(10, 26);
			Reactor1Adorner = new HaloGameObject(Color.OrangeRed, 0.2f, 0.5f);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(Reactor1Adorner, new Vector2(adornerPosition.X, -300), adornerPosition));
			Reactor1Adorner.Renderable.Alpha = 0.25f;

			adornerPosition = Body.Position + new Vector2(110, 26);
			Reactor2Adorner = new HaloGameObject(Color.OrangeRed, -0.2f, 0.5f);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(Reactor2Adorner, new Vector2(adornerPosition.X, -300), adornerPosition));
			Reactor2Adorner.Renderable.Alpha = 0.25f;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			HaloAdorner.Body.MoveTo(Body.Position + Size * 0.5f);
			Reactor1Adorner.Body.MoveTo(Body.Position + new Vector2(10, 26));
			Reactor2Adorner.Body.MoveTo(Body.Position + new Vector2(110, 26));
			LeftTrailParticlesEmitter.Emit(gameTime);
			RightTrailParticlesEmitter.Emit(gameTime);
		}
	}
}