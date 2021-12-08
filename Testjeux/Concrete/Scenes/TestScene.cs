using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class TestScene : BaseScene
	{
		private static readonly Point BALL = new Point(24);

		protected Point Screen;

		public TestScene() : base()
		{
			Screen = ServiceLocator.Instance.Get<IScreenService>().GetScreenSize();

			// Murs autour de la scène
			RegisterGameObject(new WallGameObject(new Vector2(-50), new Vector2(Screen.X + 100, 50)));
			RegisterGameObject(new WallGameObject(new Vector2(-50, Screen.Y), new Vector2(Screen.X + 100, 50)));
			RegisterGameObject(new WallGameObject(new Vector2(-50, 0), new Vector2(50, Screen.Y)));
			RegisterGameObject(new WallGameObject(new Vector2(Screen.X, 0), new Vector2(50, Screen.Y)));

			IRandomService rand = ServiceLocator.Instance.Get<IRandomService>();
			for (int i = 0; i < 100; i++)
			{
				RegisterGameObject(new BallGameObject(new Vector2(rand.Next() * 600 + 100, rand.Next() * 400 + 100), new Vector2(rand.Next() * 600 - 300, rand.Next() * 600 - 300), BALL.ToVector2()));
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}
	}
}
