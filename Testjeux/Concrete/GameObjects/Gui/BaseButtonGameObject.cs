using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BaseButtonGameObject : BaseGameObject, IButtonGameObject
	{
		protected readonly IRenderable TextRenderable;
		protected readonly float BaseAlpha;
		protected float CurrentHoverTtl;
		protected readonly float MaxHoverTtl;
		protected float InitialAlpha;
		protected float TargetAlpha;
		protected float AlphaDirection;
		protected bool HoverWip;
		protected bool mHover;
		protected readonly ITweening HoverTweening;

		public bool Hover 
		{
			get => mHover;
			set
			{
				if (mHover != value)
				{
					mHover = value;
					Services.Instance.Get<IAssetService>().GetSound(SoundName.Hover).Play();
					HoverWip = true;
					InitialAlpha = Renderable.Alpha;
					if (mHover)
					{
						CurrentHoverTtl = 0.0f;
						TargetAlpha = 1.0f;

					}
					else
					{
						CurrentHoverTtl = MaxHoverTtl;
						TargetAlpha = BaseAlpha;
					}
					AlphaDirection = TargetAlpha - InitialAlpha;
					AlphaDirection /= Math.Abs(AlphaDirection);
				}
			}
		}

		protected BaseButtonGameObject(Texture2D texture, Vector2 position, string text, Color textColor, ICommand command)
		{
			HoverTweening = Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut);
			MaxHoverTtl = 0.15f;
			BaseAlpha = 0.5f;
			Hover = false;
			Vector2 size = new(texture.Width, texture.Height);
			Type = GameObjectType.BUTTON;
			SpriteFont font = Services.Instance.Get<IAssetService>().GetFont(FontName.Button);
			Vector2 textSize = font.MeasureString(text);
			Body = new BoxBody(position, size, Vector2.Zero, 1.0f, true, new ButtonWrapperColliderCommand(this, command));
			TextRenderable = new TextRenderable(this, new Vector2((size.X - textSize.X) / 2, (size.Y - textSize.Y) / 2 - 5), font, text, textColor)
			{
				Alpha = BaseAlpha,
				Layer = 0.81f
			};
			Renderable = new TextureRenderable(this, texture, 1.0f, Vector2.Zero)
			{
				Alpha = BaseAlpha,
				Layer = 0.8f
			};
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			IGameObject cursor = Services.Instance.Get<ISceneService>().GetObject(item => item is CursorGameObject);
			Hover = ((null != cursor) && (null != Services.Instance.Get<IColliderService>().IsCollision(Body, cursor.Body)));
			if (HoverWip)
			{
				CurrentHoverTtl += AlphaDirection * (float)gameTime.ElapsedGameTime.TotalSeconds;
				if ((CurrentHoverTtl < 0.0f) || (CurrentHoverTtl > MaxHoverTtl))
				{
					TextRenderable.Alpha = Renderable.Alpha = TargetAlpha;
					HoverWip = false;
				}
				else
				{
					TextRenderable.Alpha = Renderable.Alpha = BaseAlpha * (1 + HoverTweening.GetStep(CurrentHoverTtl / MaxHoverTtl));
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			TextRenderable.Draw(spriteBatch);
		}
	}
}