using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public interface IGameplayStateScene
	{
		void Enter();
		void Exit();
		void Load();
		void Update(GameTime gameTime);
	}
}