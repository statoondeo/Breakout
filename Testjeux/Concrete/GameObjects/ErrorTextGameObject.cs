using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class ErrorTextGameObject : TextGameObject
	{
		private Vector2 Origin;
		private Vector2 Destination;
		private bool Returned;
		private float Ttl;

		public ErrorTextGameObject(Vector2 position, SpriteFont spriteFont, string text, Color textColor)
			: this(position, spriteFont, text, textColor, 0.0f)
		{
		}

		public ErrorTextGameObject(Vector2 position, SpriteFont spriteFont, string text, Color textColor, float angle)
			: base(position, spriteFont, text, textColor, angle)
		{
			Renderable.Layer = 0.9f;
			Movable = new TweeningMovable(this);
			Destination = position;
			Origin = new Vector2(Destination.X, -300);
			(Movable as TweeningMovable).Init(Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), 1.0f, Origin, Destination);
			Returned = false;
			Ttl = 0.0f;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (Returned)
			{
				if ((Movable as TweeningMovable).Ended)
				{
					Status = GameObjectStatus.OUTDATED;
				}
			}
			else
			{
				if (Ttl != 0)
				{
					Ttl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
					if (Ttl < 0.0f)
					{
						Ttl = 0.0f;
						Returned = true;
						(Movable as TweeningMovable).Init(Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintIn), 1.0f, Destination, Origin);
					}
				}
				else
				{
					if ((Movable as TweeningMovable).Ended)
					{
						Ttl = 3.0f;
					}
				}
			}
		}
	}
}