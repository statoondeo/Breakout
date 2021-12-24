using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrainShield2Trigger : BaseTriggerGameObject
	{
		public BrainShield2Trigger()
			: base(new BrainShield2TriggerCommand(), false)
		{
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (Services.Instance.Get<ISceneService>().GetObjects(item => item is WobblerGameObject && item.Status == GameObjectStatus.ACTIVE).Count == 0)
			{
				Command.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
