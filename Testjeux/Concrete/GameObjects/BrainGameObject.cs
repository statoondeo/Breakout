using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class BrainGameObject : BaseBrickGameObject, IStateContainer
	{
		public static readonly float BodySizeFactor = 0.65f;
		public static readonly Vector2 TextureSize = new(256);

		private IStateItem mCurrentState;
		public IStateItem CurrentState {
			get => mCurrentState;
			set
			{
				(mCurrentState as BrainState)?.Exit();
				mCurrentState = value;
				(mCurrentState as BrainState).Enter();
			}
		}

		public BrainGameObject(Vector2 position, float scale)
			: base(position, TextureSize.X * 0.5f * scale * BodySizeFactor, 10)
		{
			Vector2 offset = (TextureSize * BodySizeFactor - TextureSize) * 0.5f + new Vector2(-5, 20);
			Body = new BrickBody(position, TextureSize.X * 0.5f * scale * BodySizeFactor, new BrainColliderCommand(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 25)));

			IStateItem idleState = new BrainIdleState(this, offset);
			IStateItem attackState = new BrainAttackState(this, offset);

			idleState.Transitions.Add(attackState);
			attackState.Transitions.Add(idleState);

			CurrentState = idleState;

			Services.Instance.Get<ISceneService>().RegisterGameObject(new BrainWinTrigger());
		}

		public override void Damage()
{
			if ((CurrentState as BrainState).Damage())
			{
				base.Damage();
			}
			Services.Instance.Get<IAssetService>().GetSound(SoundName.Explosion3).Play();
		}

		public override void Update(GameTime gameTime)
		{
			(CurrentState as BrainState).Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			(CurrentState as BrainState).Draw(spriteBatch);
		}
	}
}