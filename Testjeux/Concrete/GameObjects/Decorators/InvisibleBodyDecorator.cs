using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class InvisibleBodyDecorator : BaseGameObjectDecorator
	{
		public InvisibleBodyDecorator(IGameObject gameObject) 
			: base(gameObject)
		{
			Body = new InvisibleBodyWrapper(DecoratedGameObject.Body);
		}

		public override IBody Body { get; set; }
		public override GameObjectStatus Status { get; set; }

		public override void Update(GameTime gameTime)
		{
			DecoratedGameObject.Renderable.Update(gameTime);
		}
	}
}