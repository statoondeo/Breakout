using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class FlashScreenGameObject : BaseGameObject
	{
		private readonly float Ttl;
		private float CurrentTtl;

		public FlashScreenGameObject()
			: base()
		{
			Type = GameObjectType.NONE;
			Renderable = new TextureRenderable(this, Services.Instance.Get<IShapeService>().CreateTexture(Services.Instance.Get<IScreenService>().GetScreenSize(), Color.White), 1.0f, Vector2.Zero)
			{
				Layer = 0.5f
			};
			CurrentTtl = Ttl = 1.0f;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			CurrentTtl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (CurrentTtl < 0.0f)
			{
				Status = GameObjectStatus.OUTDATED;
			}
			else
			{
				Renderable.Alpha = Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintIn).GetStep(CurrentTtl / Ttl);
			}
		}
	}
}