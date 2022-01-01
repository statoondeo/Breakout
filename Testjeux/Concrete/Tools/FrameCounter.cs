using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class FrameCounter
	{
		public int Count { get; protected set; }

		protected CircularArray<double> Frames;

		public FrameCounter()
		{
			Count = 0;
			Frames = new CircularArray<double>(60);
		}

		public void Reset()
		{
			for (int i = 0; i < 60; i++)
			{
				Frames.Set(0.0);
			}
		}
		protected double PerformAverage()
		{
			double sum = 0.0;
			for (int i = 0; i < 60; i++)
			{
				sum += Frames.Get(i);
			}
			return (sum / 60);
		}

		public void Update(GameTime gameTime)
		{
			Frames.Set(gameTime.ElapsedGameTime.TotalSeconds);
			Count = (int)Math.Ceiling(1.0 / PerformAverage());
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(Services.Instance.Get<IAssetService>().GetFont(FontName.Mini), Count.ToString(), new Vector2(1248.0f, 8.0f), Color.White);
		}
	}
}