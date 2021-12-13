using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrainWinTrigger : BaseTriggerGameObject
	{
		public BrainWinTrigger()
			: base(new WinTriggerCommand(), false)
		{
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (ServiceLocator.Instance.Get<ISceneService>().GetObjects(item => item is BrainBrickGameObject && item.Status == GameObjectStatus.ACTIVE).Count == 0)
			{
				Command.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
