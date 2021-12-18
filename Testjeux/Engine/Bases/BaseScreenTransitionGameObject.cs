using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public abstract class BaseScreenTransitionGameObject : BaseGameObject, IScreenTransitionGameObject
	{
		protected float MaxTtl;
		protected float CurrentTtl;
		protected float DirectionTtl;
		protected ICommand WhenEndedCommand;

		protected BaseScreenTransitionGameObject(float ttl, ScreenTransitionDirection direction, ICommand whenEndedCommand)
			: base()
		{
			MaxTtl = 1;
			CurrentTtl = direction == ScreenTransitionDirection.Show ? MaxTtl : 0;
			DirectionTtl = direction == ScreenTransitionDirection.Show ? -1 : 1;
			WhenEndedCommand = whenEndedCommand;
			Type = GameObjectType.NONE;
			Renderable = new TextureRenderable(this, Services.Instance.Get<IShapeService>().CreateTexture(Services.Instance.Get<IScreenService>().GetScreenSize(), Color.Black), 1.0f, Vector2.Zero);
			Renderable.Alpha = CurrentTtl / MaxTtl;
		}

		public bool Ended => ((CurrentTtl < 0) || (CurrentTtl > MaxTtl));

		public override void Update(GameTime gameTime)
		{
			if (Ended)
			{
				Renderable.Alpha = DirectionTtl == 1 ? 1.0f : 0.0f;
				WhenEndedCommand.Execute();
			}
		}
	}
}