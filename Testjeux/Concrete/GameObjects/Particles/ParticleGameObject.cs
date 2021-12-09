using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{

	public class ParticleGameObject : BaseGameObject
	{
		protected float CurrentTtl;
		protected float MaxTtl;
		protected float Angle;
		protected float AngleSpeed;
		protected float Scale;
		protected float InitialAlpha;
		protected float CurrentAlpha;
		protected Vector2 RotationOrigin;

		public ParticleGameObject() : base() 
		{
			Movable = new TweeningMovable(this);
			Renderable = new TextureRenderable(this, null);
		}

		public void Init(Texture2D texture, ITweening tweening, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed, float initialAlpha, Vector2 rotationOrigin)
		{
			Status = GameObjectStatus.ACTIVE;
			Body.MoveTo(position);
			Body.Velocity = velocity;
			CurrentTtl = MaxTtl = ttl;
			(Movable as TweeningMovable).Init(tweening, ttl, Body.Position, Body.Position + Body.Velocity * MaxTtl);
			Angle = 0;
			AngleSpeed = angleSpeed;
			CurrentAlpha = InitialAlpha = initialAlpha;
			Scale = scale;
			RotationOrigin = rotationOrigin;
			(Renderable as TextureRenderable).Texture = texture;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			Angle += AngleSpeed * dt;
			CurrentTtl -= dt;
			CurrentAlpha = CurrentTtl / MaxTtl * InitialAlpha;
			if (CurrentTtl < 0)
			{
				Status = GameObjectStatus.OUTDATED;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch, CurrentAlpha, Angle, Scale, RotationOrigin);
		}
	}
}