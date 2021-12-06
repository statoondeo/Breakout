using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class TestScene : BaseScene
	{
		private static readonly Point BALL = new Point(24);

		protected Rectangle Screen;

		public TestScene() : base()
		{
			Screen = ServiceLocator.Instance.Get<Game>().Window.ClientBounds;

			// Murs autour de la scène
			RegisterGameObject(new WallGameObject(new Vector2(-50), new Vector2(Screen.Width + 100, 50)));
			RegisterGameObject(new WallGameObject(new Vector2(-50, Screen.Height), new Vector2(Screen.Width + 100, 50)));
			RegisterGameObject(new WallGameObject(new Vector2(-50, 0), new Vector2(50, Screen.Height)));
			RegisterGameObject(new WallGameObject(new Vector2(Screen.Width, 0), new Vector2(50, Screen.Height)));

			Random rand = new Random();
			for (int i = 0; i < 100; i++)
			{
				RegisterGameObject(new BallGameObject(new Vector2((float)rand.NextDouble() * 600 + 100, (float)rand.NextDouble() * 400 + 100), new Vector2((float)rand.NextDouble() * 600 - 300, (float)rand.NextDouble() * 600 - 300), BALL.ToVector2()));
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}
	}
}
