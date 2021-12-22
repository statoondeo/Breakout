using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class SnakeBodyGameObject : BaseBrickGameObject
	{
		private Vector2 PreviousPosition;
		private readonly IGameObject RedHalo;
		private bool HaloActivated;
		private readonly IParticlesEmitter ParticlesEmitter;
		private readonly int FrameDelay;
		private readonly CircularArray<Vector2> FramePositions;
		private readonly IParticlesEmitter TrailParticlesEmitter;
		public IList<float> Waves { get; set; }

		public SnakeBodyGameObject(Vector2 position, CircularArray<Vector2> framePositions, int frameDelay)
			: base(position, 32 * 0.6f, 8)
		{
			Renderable = new SnakeBodyRenderable(this)
			{
				Offset = new Vector2(32 * 0.6f),
				Layer = 0.55f
			};
			PreviousPosition = Vector2.Zero;
			Body.CollideCommand = new SnakeBodyColliderCommand(this, new DummyParticlesEmitter());
			Body.RotationOrigin = new Vector2(32);
			RedHalo = new HaloGameObject(Color.DeepPink);
			RedHalo.Renderable.Alpha = 0.5f;
			Waves = new List<float>();
			MaxDamageTtl = 0.2f;
			HaloActivated = false;
			ParticlesEmitter = new SnakeExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.PurpleSpark), 75);
			FrameDelay = frameDelay;
			FramePositions = framePositions;
			TrailParticlesEmitter = new SnakeTrailParticlesEmitter(this, 0.25f);
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
			RedHalo.Body.MoveTo(Body.Position);
			Body.Angle = (float)Math.Atan2((double)(Body.Position.Y - PreviousPosition.Y), (double)(Body.Position.X - PreviousPosition.X));
			for (int i = 0; i < Waves.Count; i++)
			{
				float item = Waves[i];
				item -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				Waves[i] = item;
				if (item < 0.0f)
				{
					Damage();
					SnakeHeadGameObject serpentHead = Services.Instance.Get<ISceneService>().GetObject(item => item is SnakeHeadGameObject) as SnakeHeadGameObject;
					if (this == serpentHead.Bodies[serpentHead.Bodies.Count - 1])
					{
						Status = GameObjectStatus.OUTDATED;
						ParticlesEmitter.Emit();
						(serpentHead.Tail as SnakeTailGameObject).FrameDelay = FrameDelay;
					}
				}
			}
			(Waves as List<float>).RemoveAll(item => item < 0.0f);
			HaloActivated = Renderable.ColorMask != Color.White;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (HaloActivated)
			{
				RedHalo.Draw(spriteBatch);
			}
			base.Draw(spriteBatch);
		}
	}
}