using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class BlobGameObject : BaseBrickGameObject
	{
		private bool Ended;
		private float CurrentTtl;
		private readonly float Ttl;

		public BlobGameObject(Vector2 position, float scale)
			: base(position, 32, 3)
		{
			Body = new BrickBody(position, 64 * 0.5f * scale * 1.0f, new BrickColliderCommand(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.GreenSpark), 25)));
			Renderable = new BlobAnimatedTextureRenderable(this, scale);
			Ended = true;
		}

		public BlobGameObject(Vector2 position, float scale, Vector2 destination, float ttl)
			: base(position, 32, 1)
		{
			Body = new BrickBody(position, 64 * 0.5f * scale * 1.0f, new BrickColliderCommand(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.PurpleSpark), 25)));
			Movable = new TweeningMovable(this);
			(Movable as TweeningMovable).Init(Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintIn), ttl, position, destination);
			Renderable = new BlobAnimatedTextureRenderable(this, scale);
			Ended = false;
			CurrentTtl = Ttl = 3.0f;
		}

		public override void Damage()
		{
			base.Damage();
			Services.Instance.Get<IAssetService>().GetSound(SoundName.Blob1).Play();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (!Ended)
			{
				CurrentTtl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				Ended = CurrentTtl < 0;
				if (Ended)
				{
					Renderable.ColorMask = new Color(238, 125, 238);
				}
				else
				{
					float step = Services.Instance.Get<ITweeningService>().Get(TweeningName.Linear).GetStep(CurrentTtl / Ttl);
					Renderable.ColorMask = new Color(17 * step + 238, 125 * step + 130, 17 * step + 238);
				}
			}

		}
	}
}