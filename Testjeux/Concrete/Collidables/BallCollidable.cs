namespace GameNameSpace
{
	public class BallCollidable : RectangleCollidable
	{
		protected ICollideCommand CollideCommand;
		public BallCollidable(IPositionable positionable, ICollideCommand collideCommand) 
			: base(positionable)
		{
			CollideCommand = collideCommand;
		}

		public override void Collide()
		{
			base.Collide();
			// On teste la collision de la balle avec tous les autres objets de la scène
			IScene scene = ServiceLocator.Instance.Get<GameState>().CurrentScene;

			foreach(IGameObject gameObject in scene.GameObjectsCollection)
			{
				if (Collider.IsCollision(this, gameObject.Collidable))
				{
					switch(gameObject.Type)
					{
						case GameObjectType.WALL:
						case GameObjectType.RACKET:
							CollideCommand.Execute(gameObject.Collidable);
							break;
						case GameObjectType.BRICK:
							gameObject.Status = GameObjectStatus.OUTDATED;
							CollideCommand.Execute(gameObject.Collidable);
							break;
					}
				}
			}
		}
	}
}