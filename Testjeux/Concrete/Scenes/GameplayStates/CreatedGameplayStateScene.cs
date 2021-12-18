using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class CreatedGameplayStateScene : BaseGameplayStateScene
	{
		protected float Ttl;
		protected Point Screen;

		public CreatedGameplayStateScene(GameplayScene gameplayScene)
			: base(gameplayScene)
		{
			Ttl = 0;
			Screen = Services.Instance.Get<IScreenService>().GetScreenSize();
		}

		public override void Load()
		{
			Services.Instance.Get<IGameObjectFactoryService>().ResetTtl();

			// Chargement du niveau
			foreach (IGameObject gameObject in Services.Instance.Get<IGameObjectFactoryService>().CreateLevel(Services.Instance.Get<ILevelService>().GetLevel(GameplayScene.Level)))
			{
				GameplayScene.RegisterGameObject(gameObject);
			}

			// Raquette du joueur
			Texture2D racketTexture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.Platform);
			Vector2 racketSize = new Vector2(racketTexture.Width, racketTexture.Height);
			Vector2 origin = new Vector2((Screen.X - racketSize.X) / 2, -300);
			Vector2 destination = new Vector2((Screen.X - racketSize.X) / 2, Screen.Y - 2 * racketSize.Y);
			GameplayScene.RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(new RacketGameObject(Vector2.Zero), origin, destination));

			Ttl = Services.Instance.Get<IGameObjectFactoryService>().MaxTtl;
		}

		public override void Update(GameTime gameTime)
		{
			Ttl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (Ttl < 0)
			{
				// On change d'état : InitializedState
				GameplayScene.GotoState(GameplayStateNames.Initialized);
			}
		}
	}
}
