using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class RacketTrailParticlesEmitter : BaseParticlesEmitter
	{
		protected Vector2 Velocity;
		protected float Angle;
		protected Vector2 Offset;
		protected float MaxTtl;
		protected float Ttl;

		public RacketTrailParticlesEmitter(IGameObject gameObject, float angle, Vector2 offset)
			: base(gameObject, Services.Instance.Get<IAssetService>().GetTexture(TextureName.Thrust), 0)
		{
			Angle = angle;
			Velocity = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
			Offset = offset;
			MaxTtl = Ttl = 0.0f;
		}

		public override void Emit(GameTime gameTime)
		{
			Ttl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (Ttl < 0)
			{
				Ttl = MaxTtl;
				IRandomService rand = Services.Instance.Get<IRandomService>();
				float ttl = rand.Next() * 0.05f + 0.025f;
				float scale = rand.Next() * 1f + 0.5f;
				float speed = (rand.Next() * 500.0f + 250.0f) / scale;
				Color mask = rand.Next() > 0.5f ? rand.Next() > 0.75f ? rand.Next() > 0.9f ? Color.White : Color.Yellow : Color.DarkOrange : Color.Red;
				Services.Instance.Get<IParticlesService>().Register(Texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.Linear), GameObject.Body.Position + Offset /*- Size * scale * 0.5f*/, Velocity * speed, scale, ttl, 0.0f, 1.0f, Angle, Size * 0.5f, mask);
			}
		}
	}
}