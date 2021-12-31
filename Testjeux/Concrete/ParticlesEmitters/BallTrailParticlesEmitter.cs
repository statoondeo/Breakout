using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BallTrailParticlesEmitter : BaseParticlesEmitter
	{
		protected float Scale;
		protected float Ttl;
		protected float AngleSpeed;

		public BallTrailParticlesEmitter(IGameObject gameObject, Texture2D texture, float scale = 2.0f, float ttl = 2.0f)
			: base(gameObject, texture, 0)
		{
			Scale = scale;
			Ttl = ttl;
		}

		public override void Emit(GameTime gameTime)
{
			float particleSpeed = 0;
			Vector2 particleVelocity = Vector2.Zero;
			Services.Instance.Get<IParticlesService>().Register(Texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), (GameObject.Body as ICircleBody).Center - Size * Scale * 0.5f, particleVelocity * particleSpeed, Scale, Ttl, 0, 0.5f, 0.0f, Vector2.Zero);
		}
	}
}