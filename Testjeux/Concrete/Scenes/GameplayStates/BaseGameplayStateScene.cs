using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public abstract class BaseGameplayStateScene : IGameplayStateScene
	{
		protected GameplayScene GameplayScene;

		public BaseGameplayStateScene(GameplayScene gameplayScene)
		{
			GameplayScene = gameplayScene;
		}

		public virtual void Enter() { }
		public virtual void Exit() { }
		public virtual void Load() { }
		public virtual void Update(GameTime gameTime) { }
	}
}
