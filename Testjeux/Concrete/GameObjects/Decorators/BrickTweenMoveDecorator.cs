using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrickTweenMoveDecorator : TweenMoveDecorator, IBrickGameObject
	{
		public BrickTweenMoveDecorator(IBrickGameObject gameObject, ITweening tweening, Vector2 origin, Vector2 destination, float ttl, float delay) 
			: base(gameObject, tweening, origin, destination, ttl, delay)
		{
		}

		public int Health { get => (DecoratedGameObject as IBrickGameObject).Health; set => (DecoratedGameObject as IBrickGameObject).Health = value; }

		public void Damage()
		{
		}
	}
}