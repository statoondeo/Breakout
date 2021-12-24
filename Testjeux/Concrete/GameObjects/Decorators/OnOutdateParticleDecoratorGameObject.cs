using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class OnOutdateParticleDecoratorGameObject : BaseGameObjectDecorator
	{
		protected ICommand CommandWhenOudate;

		public OnOutdateParticleDecoratorGameObject(IGameObject gameObject, ICommand commandWhenOudate)
			: base(gameObject)
		{
			CommandWhenOudate = commandWhenOudate;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			(DecoratedGameObject as IParticleGameObject).CurrentAlpha = 1.0f;
			if (DecoratedGameObject.Status == GameObjectStatus.OUTDATED)
			{
				CommandWhenOudate.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}