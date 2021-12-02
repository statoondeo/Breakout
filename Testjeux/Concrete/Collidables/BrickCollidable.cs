namespace GameNameSpace
{
	public class BrickCollidable : RectangleCollidable
	{
		protected ICollideCommand CollideCommand;
		public BrickCollidable(IPositionable positionable, ICollideCommand collideCommand)
			: base(positionable)
		{
			CollideCommand = collideCommand;
		}

		public override void Collide()
		{
			base.Collide();
			// On teste la collision de la balle avec tous les autres objets de la scène
			IScene scene = ServiceLocator.Instance.Get<GameState>().CurrentScene;

			foreach (IGameObject gameObject in scene.GameObjectsCollection)
			{
				if (Collider.IsCollision(this, gameObject.Collidable))
				{
					switch (gameObject.Type)
					{
						case GameObjectType.BALL:
						case GameObjectType.MISSILE:
							CollideCommand.Execute(gameObject.Collidable);
							break;
					}
				}
			}
		}
	}
}