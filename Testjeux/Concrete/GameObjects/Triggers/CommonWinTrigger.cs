using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CommonWinTrigger : BaseTriggerGameObject
	{
		public CommonWinTrigger()
			: base(new WinTriggerCommand(), false)
		{
		}

		public override void Update(GameTime gameTime)
		{
			if (Services.Instance.Get<ISceneService>().Life <= 0)
			{
				Command.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
