using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class SingleBallGameObject : BaseBallGameObject
	{
		public SingleBallGameObject(Vector2 position, float speed, Vector2 size)
			: base(Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedBullet), position, speed, size)
		{
			Body = new BallBody(position, size * Scale * BodySizeFactor, Vector2.Zero, new BallColliderCommand(this, new BallImpactParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow))));
			TrailParticlesEmitter = new BallTrailParticlesEmitter(this, Services.Instance.Get<IShapeService>().CropTexture(Texture, new Rectangle(new Point(224, 0), new Point(32))), 1.0f, 0.5f);
		}
	}
}