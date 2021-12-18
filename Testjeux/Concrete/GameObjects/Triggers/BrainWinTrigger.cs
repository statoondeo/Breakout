using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrainWinTrigger : BaseTriggerGameObject
	{
		public BrainWinTrigger()
			: base(new WinTriggerCommand(), false)
		{
			Status = GameObjectStatus.IDLE;
		}

		public override void Update(GameTime gameTime)
		{
			if (Services.Instance.Get<ISceneService>().GetObjects(item => item is BrainBrickGameObject && item.Status == GameObjectStatus.ACTIVE).Count == 0)
			{
				Command.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
