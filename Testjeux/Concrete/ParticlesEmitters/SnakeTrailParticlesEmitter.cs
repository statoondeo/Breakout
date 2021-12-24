using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class SnakeTrailParticlesEmitter : BaseParticlesEmitter
	{
		protected float Ttl;

		public SnakeTrailParticlesEmitter(IGameObject gameObject, float ttl)
			: base(gameObject, Services.Instance.Get<IShapeService>().CropTexture(Services.Instance.Get<IAssetService>().GetTexture(TextureName.PurpleLaser), new Rectangle(Point.Zero, new Point(20))), 0)
		{
			Ttl = ttl;
		}

		public override void Emit(GameTime gameTime)
		{
			float scale = 0.8f;
			float particleSpeed = 0;
			Vector2 particleVelocity = Vector2.Zero;
			Services.Instance.Get<IParticlesService>().Register(Texture, Services.Instance.Get<ITweeningService>().Get(TweeningName.Linear), (GameObject.Body as ICircleBody).Center - Size * scale * 0.5f, particleVelocity * particleSpeed, scale, Ttl, 0, 0.5f, 0.0f, Vector2.Zero);
		}
	}
}