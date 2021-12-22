using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class ElasticZoomGameObject : BaseGameObject
	{
		protected float Ttl;
		protected float CurrentTtl;
		protected float TargetScale;
		protected ITweening Tweening;
		protected Vector2 Size;

		public ElasticZoomGameObject(IRenderable renderable, Vector2 position, Vector2 size, float ttl, float targetScale)
			: base()
{
			Size = size;
			TargetScale = targetScale;
			Body = new InvisibleBody(position);
			Ttl = ttl;
			CurrentTtl = 0;
			Tweening = Services.Instance.Get<ITweeningService>().Get(TweeningName.ElasticOut);
			Renderable = renderable;
			(Renderable as TextureRenderable).GameObject = this;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			CurrentTtl += (float)gameTime.ElapsedGameTime.TotalSeconds;
			Renderable.Scale = Tweening.GetStep(CurrentTtl / Ttl) * TargetScale;
			Renderable.Offset = Size * (1 - Renderable.Scale) * 0.5f;
			if (CurrentTtl > Ttl)
			{
				Status = GameObjectStatus.OUTDATED;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch, (Ttl - CurrentTtl) / Ttl, 0.0f, Renderable.Scale, Vector2.Zero);
		}
	}
}