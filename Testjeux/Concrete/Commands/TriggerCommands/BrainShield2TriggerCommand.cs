using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BrainShield2TriggerCommand : BaseCommand
	{
		public BrainShield2TriggerCommand() : base() { }

		public override void Execute()
		{
			ShieldBrickGameObject shield = ServiceLocator.Instance.Get<ISceneService>().GetObject(item => item is ShieldBrickGameObject && item.Status == GameObjectStatus.ACTIVE) as ShieldBrickGameObject;
			if (shield != null)
			{
				Vector2 center = shield.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * shield.Renderable.Scale * 0.25f;
				IRandomService rand = ServiceLocator.Instance.Get<IRandomService>();
				Texture2D texture = ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
				Vector2 textureSize = new Vector2(texture.Width, texture.Height);
				Vector2 position = shield.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * shield.Renderable.Scale * 0.25f;
				if (ServiceLocator.Instance.Get<ISceneService>().GetObjects(item => item is WobblerBrickGameObject && item.Status == GameObjectStatus.ACTIVE).Count == 0)
				{
					for (int i = 1; i <= 5; i++)
					{
						ServiceLocator.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new ShieldTextureRenderable(null, 1.0f, Vector2.Zero), center, ShieldBrickGameObject.TextureSize, i * 0.2f, i * 2.1f));
					}

					// Destruction du bouclier
					shield.Status = GameObjectStatus.OUTDATED;

					// On change l'animation du méchant
					BrainBrickGameObject brain = ServiceLocator.Instance.Get<ISceneService>().GetObject(item => item is BrainBrickGameObject) as BrainBrickGameObject;
					(brain.Renderable as AnimatedTextureRenderable).ChangeAnimation(new TextureAnimation(new int[] { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 }, 0.05f, true));
				}
			}
		}
	}
}
