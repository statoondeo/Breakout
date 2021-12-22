using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
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
			spriteBatch.Draw(Texture, GameObject.Body.Position + Offset, new Rectangle(SourcePosition, FrameSize), ColorMask, GameObject.Body.Angle, GameObject.Body.RotationOrigin, InitialScale, SpriteEffects.None, Layer);
		}

		public override void Draw(SpriteBatch spriteBatch, float alpha, float angle, float scale, Vector2 rotationOrigin)
		{
			spriteBatch.Draw(Texture, GameObject.Body.Position + Offset, new Rectangle(SourcePosition, FrameSize), ColorMask * alpha, angle, rotationOrigin, scale * InitialScale, SpriteEffects.None, Layer);
		}
	}
}