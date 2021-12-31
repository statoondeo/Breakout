using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(Services.Instance.Get<IAssetService>().GetFont(FontName.Mini), Count.ToString(), new Vector2(1248.0f, 8.0f), Color.White);
		}
	}
}