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
			if (Services.Instance.Get<ISceneService>().GetObject(item => item is BrainGameObject) == null)
			{
				Command.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
