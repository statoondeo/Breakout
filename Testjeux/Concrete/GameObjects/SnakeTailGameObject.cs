using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class SnakeTailGameObject : BaseBrickGameObject
	{
		private Vector2 PreviousPosition;
		public int FrameDelay { get; set; }
		private readonly CircularArray<Vector2> FramePositions;
		private readonly IParticlesEmitter TrailParticlesEmitter;

		public SnakeTailGameObject(Vector2 position, CircularArray<Vector2> framePositions, int frameDelay)
			: base(position, 32 * 0.8f, 8)
		{
			Renderable = new SankeTailRenderable(this)
			{
				Offset = new Vector2(32 * 0.8f),
				Layer = 0.55f
			};
			PreviousPosition = Vector2.Zero;
			Body.RotationOrigin = new Vector2(32);
			FramePositions = framePositions;
			FrameDelay = frameDelay;
			TrailParticlesEmitter = new SnakeTrailParticlesEmitter(this, 1.0f);
		}

		public override void Damage()
		{
			base.Damage();
			Health++;
		}

		public override void Update(GameTime gameTime)
		{
			PreviousPosition = Body.Position;
			base.Update(gameTime);
			TrailParticlesEmitter.Emit(gameTime);
			Body.MoveTo(FramePositions.Get(FrameDelay));
			Body.Angle = (float)Math.Atan2((double)(Body.Position.Y - PreviousPosition.Y), (double)(Body.Position.X - PreviousPosition.X));
		}
	}
}