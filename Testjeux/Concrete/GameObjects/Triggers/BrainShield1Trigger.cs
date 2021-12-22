using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrainShield1Trigger : BaseTriggerGameObject
	{
		protected int Step;
		protected ICommand Trigger2Command;

		public BrainShield1Trigger()
			: base(new BrainShield1TriggerCommand(), false)
		{
			Step = 1;
			Trigger2Command = new BrainShield2TriggerCommand();
		}

		public override void Update(GameTime gameTime)
		{
			int wobblerCount = Services.Instance.Get<ISceneService>().GetObjects(item => item is WobblerBrickGameObject).Count;
			if ((Step == 1) && (wobblerCount == 1))
			{
				Step++;
				Command.Execute();
			}
			else if ((Step == 2) && (wobblerCount == 0))
			{
				Trigger2Command.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
