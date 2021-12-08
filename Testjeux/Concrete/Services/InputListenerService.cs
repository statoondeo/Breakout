using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameNameSpace
{
	public class InputListenerService : IInputListenerService
	{
		// Gestion du clavier
		protected KeyboardState OldKeyboardState;
		protected KeyboardState NewKeyboardState;

		// Gestion de la souris
		protected MouseState OldMouseState;
		protected MouseState NewMouseState;

		protected bool FrameDone;

		public InputListenerService()
		{
			FrameDone = false;
		}

		public void Update(GameTime gameTime)
		{
			OldKeyboardState = NewKeyboardState;
			OldMouseState = NewMouseState;
			FrameDone = false;
		}

		// Gestion du clavier
		public bool IsKeyPressed(Keys key)
		{
			return (!OldKeyboardState.IsKeyDown(key) && GetKeyboardState().IsKeyDown(key));
		}
		public bool IsKeyDown(Keys key)
		{
			return (GetKeyboardState().IsKeyDown(key));
		}

		// Gestion de la souris
		public bool IsLeftClick()
		{
			return ((GetMouseState().LeftButton == ButtonState.Released) && (OldMouseState.LeftButton == ButtonState.Pressed));
		}

		public Point MousePosition()
		{
			return (GetMouseState().Position);
		}

		protected void RefreshStates()
		{
			NewKeyboardState = Keyboard.GetState();
			NewMouseState = Mouse.GetState();
			FrameDone = true;
		}

		public KeyboardState GetKeyboardState()
		{
			if (FrameDone)
			{
				return (NewKeyboardState);
			}
			RefreshStates();
			return (NewKeyboardState);
		}

		public MouseState GetMouseState()
		{
			if (!FrameDone)
			{
				RefreshStates();
			}
			return (NewMouseState);
		}
	}
}
