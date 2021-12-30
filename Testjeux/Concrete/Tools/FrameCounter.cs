using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class FrameCounter
	{
		public int Count { get; protected set; }

		public FrameCounter()
		{
			Count = 0;
		}

		public void Update(GameTime gameTime)
		{
			Count = (int)Math.Ceiling(1.0 / gameTime.ElapsedGameTime.TotalSeconds);
		}
	}
}