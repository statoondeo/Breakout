using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BlobWinTrigger : BaseTriggerGameObject
	{
		public BlobWinTrigger()
			: base(new WinTriggerCommand(), false)
		{
		}

		public override void Update(GameTime gameTime)
		{
			if (Services.Instance.Get<ISceneService>().GetObjects(item => item is TweenMoveDecorator || item is BlobGameObject || item is ForkBlobBrickGameObject).Count == 0)
			{
				Command.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
