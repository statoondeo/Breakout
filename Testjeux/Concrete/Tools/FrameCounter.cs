using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class FrameCounter
	{
		public int Count { get; protected set; }

		protected CircularArray<double> Frames;
		protected double Sum;
		protected readonly int DepthAverage;

		public FrameCounter()
		{
			Count = 0;
			Sum = 0.0;
			DepthAverage = 60;
			Frames = new CircularArray<double>(DepthAverage);
		}

		public void Reset()
		{
			for (int i = 0; i < DepthAverage; i++)
			{
				Frames.Set(0.0);
			}
		}

		public void Update(GameTime gameTime)
		{
			Sum -= Frames.Get(0);
			Frames.Set(gameTime.ElapsedGameTime.TotalSeconds);
			Sum += Frames.Get(1);
			float average = (float)Sum / DepthAverage;
			Count = (int)Math.Ceiling(1.0 / (average == 0 ? 1.0 : average));
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			//spriteBatch.DrawString(Services.Instance.Get<IAssetService>().GetFont(FontName.Mini), Count.ToString(), new Vector2(1200.0f, 8.0f), Color.White);
		}
	}
}