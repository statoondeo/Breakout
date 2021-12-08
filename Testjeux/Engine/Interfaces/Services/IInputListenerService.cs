using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameNameSpace
{
	public interface IInputListenerService : IService
	{
		KeyboardState GetKeyboardState();
		MouseState GetMouseState();
		bool IsKeyDown(Keys key);
		bool IsKeyPressed(Keys key);
		bool IsLeftClick();
		Point MousePosition();
		void Update(GameTime gameTime);
	}
}

