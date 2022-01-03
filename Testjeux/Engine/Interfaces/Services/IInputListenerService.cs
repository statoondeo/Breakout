using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameNameSpace
{
	public interface IInputListenerService : IService
	{
		float Ratio { get; set; }
		KeyboardState GetKeyboardState();
		MouseState GetMouseState();
		bool IsKeyDown(Keys key);
		bool IsKeyPressed(Keys key);
		bool IsLeftClick();
		Point MousePosition();
		void Update(GameTime gameTime);
	}
}

