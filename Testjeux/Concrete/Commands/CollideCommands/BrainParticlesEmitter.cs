using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrainParticlesEmitter : BaseParticlesEmitter
	{
		public BrainParticlesEmitter(IGameObject gameObject) 
			: base(gameObject, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 15)
		{
		}

		public override void Emit(CollisionTestResult collisionResult)
		{
			base.Emit(collisionResult);
			IRandomService rand = Services.Instance.Get<IRandomService>();
			Vector2 textureSize = new(Texture.Width, Texture.Height);
			float angleSpeed = 0;
			float ttl;
			float scale;
			float particleSpeed;
			float particleAngle;

			for (int i = 0; i < 5; i++)
			{
				ttl = rand.Next() * 0.8f + 0.2f;
				scale = rand.Next() * 1.2f + 0.3f;
				particleSpeed = rand.Next() * 200.0f / scale;
				particleAngle = rand.Next() * 2.0f * (float)Math.PI;

				Services.Instance.Get<IParticlesService>().Register(Texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), GameObject.Body.Position - textureSize * scale * 0.5f, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, angleSpeed, 1.0f, 0.0f, Vector2.Zero);
			}
		}
	}
}