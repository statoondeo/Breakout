using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseBallGameObject : BaseGameObject, IBallGameObject
	{
		protected static readonly float Scale = 2.0f;
		protected static readonly float BodySizeFactor = 0.5f;

		protected IParticlesEmitter TrailParticlesEmitter;
		protected readonly IParticlesEmitter BallExplosionParticlesEmitter;
		protected readonly IGameObject BallHalo;
		protected readonly Texture2D Texture;

		public override GameObjectStatus Status 
		{ 
			get => base.Status;
			set
			{
				base.Status = value;
				if (BallHalo != null)
				{
					BallHalo.Status = value;
				}
			}
		}

		public bool Exploded { get; set; }

		public float Speed { get; private set; }

		protected BaseBallGameObject(Texture2D texture, Vector2 position, float speed, Vector2 size)
			: base()
		{
			Texture = texture;
			Vector2 offset = (size * BodySizeFactor - size);
			Speed = speed;
			Type = GameObjectType.BALL;
			Body = new BallBody(position, size * Scale * BodySizeFactor, Vector2.Zero, new MultiBallColliderCommand(this, new BallImpactParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow))));
			Movable = new VelocityMovable(this);
			Renderable = new AnimatedTextureRenderable(this, texture, offset, new Point(32), Scale, new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, 0.05f, true));
			BallExplosionParticlesEmitter = new BallExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 25);
			Exploded = false;

			BallHalo = new HaloGameObject(Color.OrangeRed, 0.15f, 1.0f);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(BallHalo, new Vector2(position.X, -300), position));
			BallHalo.Renderable.Alpha = 0.1f;
		}

		public override void Update(GameTime gameTime)
		{
			BallHalo.Body.MoveTo(Body.Position + new Vector2(32));
			base.Update(gameTime);
			Vector2 normalizedVelocity = Vector2.Normalize(Body.Velocity);
			float currentSpeed = Vector2.Dot(normalizedVelocity, Body.Velocity);
			if (currentSpeed > Speed)
			{
				Body.Velocity = normalizedVelocity * Speed;
			}
			TrailParticlesEmitter.Emit(gameTime);
			if (Exploded)
			{
				BallExplosionParticlesEmitter.Emit();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}