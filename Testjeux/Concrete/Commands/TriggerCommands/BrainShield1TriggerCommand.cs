using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BrainShield1TriggerCommand : BaseCommand
	{
		public BrainShield1TriggerCommand() : base() { }

		public override void Execute()
		{
			ShieldBrickGameObject shield = Services.Instance.Get<ISceneService>().GetObject(item => item is ShieldBrickGameObject) as ShieldBrickGameObject;
			Vector2 center = shield.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * shield.Renderable.Scale * 0.25f;
			for (int i = 1; i <= 3; i++)
			{
				Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new ShieldTextureRenderable(null, 1.0f, Vector2.Zero), center, ShieldBrickGameObject.TextureSize, i * 0.2f, i * 2.1f));
			}
		}
	}
}

