using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrainShield1Trigger : BaseTriggerGameObject
	{
		public BrainShield1Trigger()
			: base(new BrainShield1TriggerCommand(), false)
		{
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (ServiceLocator.Instance.Get<ISceneService>().GetObjects(item => item is WobblerBrickGameObject && item.Status == GameObjectStatus.ACTIVE).Count == 1)
			{
				Command.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
