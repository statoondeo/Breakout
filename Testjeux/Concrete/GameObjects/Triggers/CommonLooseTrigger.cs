using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class CommonLooseTrigger : BaseTriggerGameObject
	{
		public CommonLooseTrigger()
			: base(new LooseTriggerCommand(), false)
		{
		}

		public override void Update(GameTime gameTime)
		{
			if ((Services.Instance.Get<ISceneService>().CurrentScene as GameplayScene).Life <= 0)
			{
				Command.Execute();
				Status = GameObjectStatus.OUTDATED;
			}
		}
	}
}
