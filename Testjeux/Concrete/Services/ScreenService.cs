using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class ScreenService : IScreenService
	{
		protected Rectangle Screen;
		public ScreenService(Rectangle screen)
		{
			Screen = screen;
		}

		public Point GetScreenSize()
		{
			return (Screen.Size);
		}
	}
}
