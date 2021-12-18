using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class TextureAnimation
	{
		protected int[] FrameList;
		protected float Speed;
		protected bool Started;
		protected bool Loop;
		protected float CurrentTtl;
		protected int CurrentFrame;
		public int Frame => FrameList[CurrentFrame];

		public TextureAnimation(int[] frameList, float speed, bool loop = true)
		{
			FrameList = frameList;
			Speed = speed;
			CurrentFrame = 0;
			Started = false;
			CurrentTtl = 0;
			Loop = loop;
		}

		public void Start(int frame = 0)
		{
			Started = true;
			CurrentTtl = frame * Speed;
			CurrentFrame = frame;
		}

		public void Update(GameTime gameTime)
		{
			if (Started)
			{
				CurrentTtl += (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (CurrentTtl > (CurrentFrame + 1) * Speed)
				{
					CurrentFrame++;
					if (CurrentFrame >= FrameList.Length)
					{
						if (Loop)
						{
							Start();
						}
						else
						{
							Started = false;
							CurrentFrame = FrameList.Length - 1;
						}
					}
				}
			}
		}
	}
}