using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public sealed class GameplayCreatedState : GameplayBaseState
	{
		private float Ttl;
		private Point Screen;

		public GameplayCreatedState(GameplayScene gameplayScene)
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
			ParsedLevel level = Services.Instance.Get<ILevelService>().GetLevel((Container as GameplayScene).Level);
			(Container as GameplayScene).Music = factory.CreateMusic(level.Music);
			foreach (IGameObject gameObject in factory.CreateLevel(level))
			{
				(Container as IScene).RegisterGameObject(gameObject);
			}

			// Raquette du joueur
			Texture2D racketTexture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.Platform);
			Vector2 racketSize = new Vector2(racketTexture.Width, racketTexture.Height);
			Vector2 origin = new Vector2((Screen.X - racketSize.X) / 2, -300);
			Vector2 destination = new Vector2((Screen.X - racketSize.X) / 2, Screen.Y - 2 * racketSize.Y);
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
