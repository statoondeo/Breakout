using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class MultiBallGameObject : BaseGameObjectDecorator, IBallGameObject
	{
		private readonly IParticlesEmitter TrailParticlesEmitter;
		private readonly IGameObject BallHalo;

		public bool Exploded { get => (DecoratedGameObject as IBallGameObject).Exploded; set => (DecoratedGameObject as IBallGameObject).Exploded = value; }
		public float Speed => (DecoratedGameObject as IBallGameObject).Speed;
		private GameObjectStatus mStatus;
		public override GameObjectStatus Status 
		{
			get => mStatus;
			set
			{
				mStatus = value;
				BallHalo.Status = mStatus;
			}
		}
		public override IBody Body { get; set; }
		public override IMovable Movable { get; set; }
		public override IRenderable Renderable { get; set; }

		public MultiBallGameObject(IGameObject ballGameObject)
			: base(ballGameObject)
		{
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.BlueBullet);
			Body = new BallBody(DecoratedGameObject.Body.Position, new Vector2(32) * 2.0f * 0.5f, DecoratedGameObject.Body.Velocity, new MultiBallColliderCommand(this, new BallImpactParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow))));
			Movable = new VelocityMovable(this);
			Renderable = new AnimatedTextureRenderable(this, texture, -0.5f * new Vector2(32), new Point(32), 2.0f, new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, 0.05f, true));
			TrailParticlesEmitter = new BallTrailParticlesEmitter(this, Services.Instance.Get<IShapeService>().CropTexture(texture, new Rectangle(new Point(224, 0), new Point(32))), 1.0f, 0.5f);

			BallHalo = Services.Instance.Get<ISceneService>().RegisterGameObject(new HaloGameObject(Color.CornflowerBlue, 0.15f, 1.0f));
			BallHalo.Renderable.Alpha = 0.1f;

			Status = GameObjectStatus.ACTIVE;
		}

		public override void Update(GameTime gameTime)
		{
			Movable.Move(gameTime);
			Renderable.Update(gameTime);

			BallHalo.Body.MoveTo(Body.Position + new Vector2(32));
			Vector2 normalizedVelocity = Vector2.Normalize(Body.Velocity);
			float currentSpeed = Vector2.Dot(normalizedVelocity, Body.Velocity);
			if (currentSpeed > Speed)
			{
				Body.Velocity = normalizedVelocity * Speed;
			}
			TrailParticlesEmitter.Emit(gameTime);
			if (Exploded)
			{
				BallHalo.Status = Status = GameObjectStatus.OUTDATED;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			BallHalo.Draw(spriteBatch);
			Renderable.Draw(spriteBatch);
		}
	}
}