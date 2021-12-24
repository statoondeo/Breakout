using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class SelectionScene : BaseScene
	{
		public SelectionScene() : base() { }

		public override void Load(ICommand commandWhenLoaded)
		{
			base.Load(commandWhenLoaded);

			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars01), new Vector2(-15, 0)));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars02), new Vector2(-7, 0)));
			RegisterGameObject(new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Stars03), new Vector2(-3, 0)));
			RegisterGameObject(new RotatingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture(TextureName.Gas3), 0.01f));

			//Titre de la scène
			RegisterGameObject(new TextGameObject(new Vector2(125, 75), Services.Instance.Get<IAssetService>().GetFont(FontName.Title), "Sélection du niveau", Color.White));

			// Présentation des niveaux
			RegisterGameObject(new BigPanelGameObject());
			RegisterGameObject(new CardPanelGameObject(new Vector2(168, 220)));
			RegisterGameObject(new CardPanelGameObject(new Vector2(533, 220)));
			RegisterGameObject(new CardPanelGameObject(new Vector2(898, 220)));

			// On ajoute les sprites des boss
			RegisterGameObject(new BrainMiniatureGameObject(new Vector2(215, 320)));
			RegisterGameObject(new BlobMiniatureGameObject(new Vector2(600, 330)));
			RegisterGameObject(new SnakeMiniatureGameObject(new Vector2(943, 303)));

			// Textes
			RegisterGameObject(new TextGameObject(new Vector2(220, 260), Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "Niveau 1", Color.White));
			RegisterGameObject(new TextGameObject(new Vector2(575, 260), Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "Niveau 2", Color.White));
			RegisterGameObject(new TextGameObject(new Vector2(940, 260), Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "Niveau 3", Color.White));

			RegisterGameObject(new TextGameObject(new Vector2(237, 450), Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "BRAIN", Color.White));
			RegisterGameObject(new TextGameObject(new Vector2(605, 450), Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "BLOB", Color.White));
			RegisterGameObject(new TextGameObject(new Vector2(960, 450), Services.Instance.Get<IAssetService>().GetFont(FontName.Base), "SNAKE", Color.White));

			// Boutons
			RegisterGameObject(new ButtonGameObject(new Vector2(125, 550), "Jouer", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY, 1)));
			RegisterGameObject(new ButtonGameObject(new Vector2(490, 550), "Jouer", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY, 2)));
			RegisterGameObject(new ButtonGameObject(new Vector2(855, 550), "Jouer", Color.Black, new SwitchSceneCommand(SceneType.GAMEPLAY, 3)));
			RegisterGameObject(new ButtonGameObject(new Vector2(125, 650), "Retour", Color.Black, new SwitchSceneCommand(SceneType.MENU)));

			// Ajout du curseur de souris
			RegisterGameObject(new CursorGameObject());

			// Animation de fond
			RegisterGameObject(new SnakeHeadGameObject(new Vector2(1600, 32)));

			RegisterGameObject(new InScreenTransitionGameObject(new CompositeCommand(commandWhenLoaded, new ResetTransitionRequiredCommand())));
		}

		public override void UnLoad(ICommand commandWhenUnloaded)
		{
			RegisterGameObject(new OutScreenTransitionGameObject(new CompositeCommand(commandWhenUnloaded, new ResetTransitionRequiredCommand())));
		}
	}
}
