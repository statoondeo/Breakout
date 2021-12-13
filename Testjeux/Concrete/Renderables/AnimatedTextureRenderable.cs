using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

		public void Start()
		{
			Started = true;
			CurrentTtl = 0;
			CurrentFrame = 0;
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
	public class AnimatedTextureRenderable : TextureRenderable
	{
		protected Point FrameSize;
		protected float InitialScale;
		protected int mCurrentFrame;
		public TextureAnimation Animation;

		protected int CurrentFrame
		{
			get => mCurrentFrame;
			set
			{
				mCurrentFrame = value;
				float frameY = mCurrentFrame / (Texture.Width / FrameSize.X);
				SourcePosition = new Point(mCurrentFrame % (Texture.Width / FrameSize.X) * FrameSize.X, (int)Math.Floor(frameY) * FrameSize.Y);
			}
		}
		protected Point SourcePosition;

		public AnimatedTextureRenderable(IGameObject gameObject, Texture2D texture, Vector2 drawOffset, Point frameSize, float scale, TextureAnimation animation) 
			: base(gameObject, texture, scale, drawOffset)
		{
			FrameSize = frameSize;
			Animation = animation;
			Animation.Start();
			CurrentFrame = Animation.Frame;
			InitialScale = scale;
		}

		public void ChangeAnimation(TextureAnimation animation)
		{
			Animation = animation;
			Animation.Start();
			CurrentFrame = Animation.Frame;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			Animation.Update(gameTime);
			CurrentFrame = Animation.Frame;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, GameObject.Body.Position + Offset, new Rectangle(SourcePosition, FrameSize), ColorMask, 0.0f, Vector2.Zero, InitialScale, SpriteEffects.None, 1.0f);
		}

		public override void Draw(SpriteBatch spriteBatch, float alpha, float angle, float scale, Vector2 rotationOrigin)
		{
			spriteBatch.Draw(Texture, GameObject.Body.Position + Offset, new Rectangle(SourcePosition, FrameSize), ColorMask * alpha, angle, rotationOrigin, scale * InitialScale, SpriteEffects.None, 1.0f);
		}
	}
}