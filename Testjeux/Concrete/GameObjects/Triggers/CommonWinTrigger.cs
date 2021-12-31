using System.Collections.Generic;
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
			IList<IGameObject> gameObjects = Services.Instance.Get<ISceneService>().GetObjects(item => (item is IBrickGameObject) && !(item is CubeGameObject) && !(item is BonusGameObject));
			if (gameObjects.Count == 0)
			{
				Services.Instance.Get<ISceneService>().Win();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
