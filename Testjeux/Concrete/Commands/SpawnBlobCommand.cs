using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class SpawnBlobCommand : BaseCommand
	{
		protected IGameObject GameObject;
		protected Vector2 Destination;
		protected float Ttl;

		public SpawnBlobCommand(IGameObject gameObject, Vector2 destination, float ttl)
			: base()
		{
			GameObject = gameObject;
			Destination = destination;
			Ttl = ttl;
		}

		public override void Execute()
		{
			Services.Instance.Get<ISceneService>().RegisterGameObject(new BlobBrickGameObject(GameObject.Body.Position, 1.0f, Destination, Ttl));
		}
	}
}