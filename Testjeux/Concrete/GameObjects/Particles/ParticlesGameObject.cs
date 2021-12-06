using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class ParticlesGameObject : BaseGameObject
	{
		protected float CurrentTtl;
		protected float MaxTtl;
		protected float Angle;
		protected float AngleSpeed;
		protected float Scale;

		public ParticlesGameObject(Texture2D texture, Vector2 position, Vector2 velocity, float scale, float ttl, float angleSpeed)
			: base()
		{
			Body.MoveTo(position);
			Body.Velocity = velocity;
			CurrentTtl = MaxTtl = ttl;
			Angle = 0;
			AngleSpeed = angleSpeed;
			Scale = scale;
			Movable = new VelocityMovable(this);
			Renderable = new TextureRenderable(this, texture);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
			Angle += AngleSpeed * dt;
			CurrentTtl -= dt;
			Status = CurrentTtl < 0 ? GameObjectStatus.OUTDATED : Status;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch, CurrentTtl / MaxTtl, Angle, Scale);
		}
	}
}