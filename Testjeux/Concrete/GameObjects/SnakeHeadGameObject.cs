using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class SnakeHeadGameObject : BaseBrickGameObject
	{
		private Vector2 PreviousPosition;
		private float Ttl;
		private bool BodyFinished;
		private bool TailFinished;
		private Vector2 InitialPosition;
		private readonly int MaxNumberOfBodies;
		private int NumberOfBodies;
		private readonly CircularArray<Vector2> TailPositions;
		private readonly IParticlesEmitter TrailParticlesEmitter;
		private readonly IGameObject Halo;

		public IList<IGameObject> Bodies { get; private set; }
		public IGameObject Tail { get; private set; }

		public SnakeHeadGameObject(Vector2 position)
			: base(position, 32.0f * 0.8f, 1)
		{
			InitialPosition = position;
			Renderable = new SnakeHeadRenderable(this)
			{
				Offset = new Vector2(32 * 0.8f),
				Layer = 0.55f
			};
			PreviousPosition = Vector2.Zero;
			Movable = new CurveMovable(this, new SnakeCurve(4.0f, 32.0f));
			Body.RotationOrigin = new Vector2(32);
			Body.CollideCommand = new SnakeHeadColliderCommand(this, new DummyParticlesEmitter());
			TailFinished = BodyFinished = false;
			Ttl = 0.25f;
			MaxNumberOfBodies = 24;
			NumberOfBodies = 0;
			Bodies = new List<IGameObject>();
			TailPositions = new CircularArray<Vector2>((int)Math.Ceiling((MaxNumberOfBodies + 2) * Ttl * 60));
			TrailParticlesEmitter = new SnakeTrailParticlesEmitter(this, 0.25f);
			Halo = new HaloGameObject(Color.DeepPink);
			Halo.Renderable.Offset += new Vector2(32 * 0.8f);
			Halo.Renderable.Alpha = 0.5f;

			Services.Instance.Get<ISceneService>().RegisterGameObject(new SnakeWinTrigger());
		}

		public override void Damage()
		{
			base.Damage();
			Health++;
			for (int i = 0; i < Bodies.Count; i++)
			{
				IGameObject item = Bodies[i];
				(item as SnakeBodyGameObject).Waves.Add((i + 1) * 0.1f);
			}
		}

		public override void Update(GameTime gameTime)
		{
			PreviousPosition = Body.Position;
			base.Update(gameTime);
			Halo.Body.MoveTo(Body.Position);
			TrailParticlesEmitter.Emit(gameTime);
			TailPositions.Set(Body.Position);
			Body.Angle = (float)Math.Atan2((double)(Body.Position.Y - PreviousPosition.Y), (double)(Body.Position.X - PreviousPosition.X));
			if (BodyFinished)
			{
				if (!TailFinished)
				{
					Ttl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
					if (Ttl < 0.0f)
					{
						Ttl = 0.25f;
						TailFinished = true;
						Tail = Services.Instance.Get<ISceneService>().RegisterGameObject(new SnakeTailGameObject(InitialPosition, TailPositions, 15 * (MaxNumberOfBodies + 2)));
					}
				}
			}
			else
			{
				Ttl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (Ttl < 0.0f)
				{
					Ttl = 0.25f;
					NumberOfBodies++;
					BodyFinished = NumberOfBodies > MaxNumberOfBodies;
					Bodies.Add(Services.Instance.Get<ISceneService>().RegisterGameObject(new SnakeBodyGameObject(InitialPosition, TailPositions, 15 * NumberOfBodies)));
				}
			}
			(Bodies as List<IGameObject>).RemoveAll(item => item.Status == GameObjectStatus.OUTDATED);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Halo.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}
	}
}