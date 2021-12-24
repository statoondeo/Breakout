using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class BrainShield2TriggerCommand : BaseCommand
	{
		public BrainShield2TriggerCommand() : base() { }

		public override void Execute()
		{
			if (Services.Instance.Get<ISceneService>().GetObject(item => item is ShieldBrickGameObject && item.Status == GameObjectStatus.ACTIVE) is ShieldBrickGameObject shield)
			{
				Vector2 center = shield.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * shield.Renderable.Scale * 0.25f;
				IRandomService rand = Services.Instance.Get<IRandomService>();
				Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
				Vector2 position = shield.Body.Position + ShieldBrickGameObject.TextureSize * ShieldBrickGameObject.BodySizeFactor * shield.Renderable.Scale * 0.25f;
				if (Services.Instance.Get<ISceneService>().GetObjects(item => item is WobblerGameObject && item.Status == GameObjectStatus.ACTIVE).Count == 0)
				{
					for (int i = 1; i <= 5; i++)
					{
						Services.Instance.Get<ISceneService>().RegisterGameObject(new ElasticZoomGameObject(new ShieldTextureRenderable(null, 1.0f, Vector2.Zero), center, ShieldBrickGameObject.TextureSize, i * 0.2f, i * 2.1f));
					}

					// Destruction du bouclier
					shield.Status = GameObjectStatus.OUTDATED;

					// On change l'animation du méchant
					BrainGameObject brain = Services.Instance.Get<ISceneService>().GetObject(item => item is BrainGameObject) as BrainGameObject;
					IStateContainer container = brain as IStateContainer;
					container.CurrentState = container.CurrentState.Transitions[0];
				}
			}
		}
	}
}
