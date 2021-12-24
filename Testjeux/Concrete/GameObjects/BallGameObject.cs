using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BallGameObject : BaseGameObject
	{
		protected static readonly float Scale = 2.0f;
		protected static readonly float BodySizeFactor = 0.5f;

		protected readonly IParticlesEmitter TrailParticlesEmitter;
		protected readonly IParticlesEmitter BallExplosionParticlesEmitter;
		protected readonly IGameObject BallHalo;

		public bool Exploded { get; set; }

		public float Speed { get; protected set; }

		public BallGameObject(Vector2 position, float speed, Vector2 size)
			: base()
		{
			Vector2 offset = (size * BodySizeFactor - size);
			Speed = speed;
			Type = GameObjectType.BALL;
			Body = new BallBody(position, size * Scale * BodySizeFactor, Vector2.Zero, new BallColliderCommand(this, new BallImpactParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow))));
			Movable = new VelocityMovable(this);
			Renderable = new AnimatedTextureRenderable(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedBullet), offset, new Point(32), Scale, new TextureAnimation(new int[] { 0, 1 ,2 ,3 ,4 ,5 ,6 ,7 ,8 ,9 ,10 ,11 ,12 ,13, 14, 15 }, 0.05f, true));
			TrailParticlesEmitter = new BallTrailParticlesEmitter(this, Services.Instance.Get<IShapeService>().CropTexture(Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedBullet), new Rectangle(new Point(224, 0), new Point(32))));
			BallExplosionParticlesEmitter = new BallExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 25);
			Exploded = false;

			BallHalo = Services.Instance.Get<ISceneService>().RegisterGameObject(new HaloGameObject(Color.OrangeRed, 0.15f, 1.0f));
			BallHalo.Renderable.Alpha = 0.1f;
		}

		public override void Update(GameTime gameTime)
		{
			BallHalo.Body.MoveTo(Body.Position + new Vector2(32));
			base.Update(gameTime);
			TrailParticlesEmitter.Emit(gameTime);
			if (Exploded)
			{
				BallExplosionParticlesEmitter.Emit();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}