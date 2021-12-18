using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BallTrailParticlesEmitter : BaseParticlesEmitter
	{
		public BallTrailParticlesEmitter(IGameObject gameObject, Texture2D texture)
			: base(gameObject, texture, 0)
		{
		}

		public override void Emit(GameTime gameTime)
		{
			IRandomService rand = Services.Instance.Get<IRandomService>();
			float ttl = 2.0f;
			float scale = 1.0f;
			float particleSpeed = 0;
			Vector2 particleVelocity = Vector2.Zero;
			Services.Instance.Get<IParticlesService>().Register(Texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), (GameObject.Body as ICircleBody).Center - Size * scale * 0.5f, particleVelocity * particleSpeed, scale, ttl, 0, 0.5f, 0.0f, Vector2.Zero);
		}
	}
}