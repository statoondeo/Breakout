using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public sealed class GameplayCreatedState : GameplayBaseState
	{
		private float Ttl;
		private Point Screen;

		public GameplayCreatedState(IStateContainer gameplayScene)
			: base(gameplayScene)
		{
			Ttl = 0;
			Screen = Services.Instance.Get<IScreenService>().GetScreenSize();
		}

		public override void Load()
		{
			Services.Instance.Get<IGameObjectFactoryService>().ResetTtl();

			// Chargement du niveau
			IGameObjectFactoryService factory = Services.Instance.Get<IGameObjectFactoryService>();
			ParsedLevel level = Services.Instance.Get<ILevelService>().GetLevel(Services.Instance.Get<ISceneService>().Level);
			(Container as GameplayScene).Music = factory.CreateMusic(level.Music);
			(Container as GameplayScene).VictoryText = level.Texts.FirstOrDefault(item => item.Type == 0).Text;
			(Container as GameplayScene).DefeatText = level.Texts.FirstOrDefault(item => item.Type == 1).Text;
			foreach (IGameObject gameObject in factory.CreateLevel(level))
			{
				(Container as IScene).RegisterGameObject(gameObject);
			}

			// Vies du joueur
			Vector2 destination;
			Vector2 origin;
			for (int i = 0; i < Services.Instance.Get<ISceneService>().Life; i++)
			{
				destination = new Vector2(32.0f * (i + 2), 8.0f);
				origin = new Vector2(destination.X, -300);
				(Container as IScene).RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(new LifeMiniatureGameObject(), origin, destination));
	}

			destination = new Vector2(12.0f);
			origin = new Vector2(destination.X, -300.0f);
			(Container as IScene).RegisterGameObject(factory.DecorateEntrance(new TextGameObject(origin, Services.Instance.Get<IAssetService>().GetFont(FontName.Mini), "Vies", Color.Silver), origin, destination));

			// Raquette du joueur
			Texture2D racketTexture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.Platform);
			Vector2 racketSize = new Vector2(racketTexture.Width, racketTexture.Height);
			destination = new Vector2((Screen.X - racketSize.X) / 2, Screen.Y - 2 * racketSize.Y);
			origin = new Vector2(destination.X, -300);
			(Container as IScene).RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(new RacketGameObject(Vector2.Zero), origin, destination));

			Ttl = Services.Instance.Get<IGameObjectFactoryService>().MaxTtl;
		}

		public override void Update(GameTime gameTime)
		{
			Ttl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (Ttl < 0)
			{
				// On change d'état : InitializedState
				Container.CurrentState = this.Transitions[0];
			}
		}
	}
}
