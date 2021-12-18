using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RacketGameObject : BaseGameObject
	{
		protected IParticlesEmitter LeftTrailParticlesEmitter;
		protected IParticlesEmitter RightTrailParticlesEmitter;
		protected IRenderable HaloRenderable;
		protected IRenderable Reactor1Renderable;
		protected IRenderable Reactor2Renderable;

		public Vector2 Size { get; protected set; }

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
			RightTrailParticlesEmitter = new RacketTrailParticlesEmitter(this, (float)Math.PI / 4, new Vector2(123, 46));

			texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
			
			HaloRenderable = new TextureRenderable(this, texture, 3.0f, 0.5f * (Size - 3.0f * (new Vector2(texture.Width, texture.Height))));
			HaloRenderable.Alpha = 0.10f;
			HaloRenderable.ColorMask = Color.LightGoldenrodYellow;

			Reactor1Renderable = new TextureRenderable(this, texture, 0.5f, (new Vector2(-8, 46) - 0.5f * (new Vector2(texture.Width, texture.Height))) * 0.5f);
			Reactor1Renderable.Alpha = 0.4f;
			Reactor1Renderable.ColorMask = Color.OrangeRed;

			Reactor2Renderable = new TextureRenderable(this, texture, 0.5f, new Vector2(123, 46) - 0.25f * (new Vector2(texture.Width, texture.Height)));
			Reactor2Renderable.Alpha = 0.4f;
			Reactor2Renderable.ColorMask = Color.OrangeRed;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			LeftTrailParticlesEmitter.Emit(gameTime);
			RightTrailParticlesEmitter.Emit(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			HaloRenderable.Draw(spriteBatch);
			Reactor1Renderable.Draw(spriteBatch);
			Reactor2Renderable.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}
	}
}