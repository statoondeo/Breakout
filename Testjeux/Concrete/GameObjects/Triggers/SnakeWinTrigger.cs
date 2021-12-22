using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class SnakeWinTrigger : BaseTriggerGameObject
	{
		public SnakeWinTrigger()
			: base(new WinTriggerCommand(), false)
		{
		}

		public override void Update(GameTime gameTime)
		{
			if (Services.Instance.Get<ISceneService>().GetObjects(item => item is SnakeBodyGameObject).Count == 0)
			{
				Command.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
