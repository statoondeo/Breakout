using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public class GameObjectFactoryService : IGameObjectFactoryService
	{
		public float MaxTtl { get; protected set; }

		public GameObjectFactoryService()
		{
			ResetTtl();
		}

		public void ResetTtl()
		{
			MaxTtl = 0;
		}

		public Vector2 ConvertToVector2(ParsedVector2 vector2)
		{
			return (new Vector2(vector2.X, vector2.Y));
		}

		public IList<IGameObject> CreateLevel(ParsedLevel jsonLevel)
		{
			ResetTtl();
			IList<IGameObject> gameObjectsCollection = new List<IGameObject>();

			// Chargement des backgrounds
			foreach (ParsedBackground background in jsonLevel.Backgrounds)
			{
				gameObjectsCollection.Add(CreateBackground(background));
			}

			// Chargement des briques
			foreach (ParsedBrick brick in jsonLevel.Bricks)
			{
				gameObjectsCollection.Add(CreateBrick(brick));
			}

			// Chargement des triggers
			foreach (ParsedTrigger trigger in jsonLevel.Triggers)
			{
				gameObjectsCollection.Add(CreateTrigger(trigger));
			}

			return (gameObjectsCollection);
		}

		public IGameObject CreateTrigger(ParsedTrigger trigger)
		{
			IGameObject gameObject = trigger.Type switch
			{
				1 => new CommonLooseTrigger(),
				2 => new CommonWinTrigger(),
				_ => throw new ArgumentNullException(nameof(trigger)),
			};
			return (gameObject);
		}

		public Song CreateMusic(ParsedMusic music)
		{
			Song song = music.Type switch
			{
				0 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.SpaceUtopia),
				1 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.SpaceDifficulties),
				2 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.SwampChase),
				3 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.SubterraneanMonster),
				4 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.BusyBeat),
				5 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.TheThroneRoom),
				6 => Services.Instance.Get<IAssetService>().GetMusic(MusicName.ZombieMarch),
				_ => throw new ArgumentNullException(nameof(music)),
			};
			return (song);
		}

		public IGameObject CreateBackground(ParsedBackground background)
		{
			IGameObject gameObject = background.Type switch
			{
				0 => new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture((TextureName)Enum.Parse(typeof(TextureName), background.Texture)), ConvertToVector2(background.Velocity)),
				1 => new RotatingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture((TextureName)Enum.Parse(typeof(TextureName), background.Texture)), background.AngleSpeed),
				_ => throw new ArgumentNullException(nameof(background)),
			};
			return (gameObject);
		}

		public IGameObject CreateNudeBrick(ParsedBrick jsonBrick)
		{
			return jsonBrick.Type switch
			{
				0 => (new WallGameObject(ConvertToVector2(jsonBrick.Position), ConvertToVector2(jsonBrick.Size))),
				1 => (new WobblerGameObject(ConvertToVector2(jsonBrick.Position), 1.0f)),// Wobbler
				2 => (new AtomGameObject(ConvertToVector2(jsonBrick.Position), 1.0f, ConvertToVector2(jsonBrick.Center), jsonBrick.Radius, jsonBrick.Angle, jsonBrick.AngleSpeed)),// Atoms rotatifs
				3 => (new CubeGameObject(ConvertToVector2(jsonBrick.Position))),// Cube
				4 => (new BrainGameObject(ConvertToVector2(jsonBrick.Position), 1.0f)),// Brain
				5 => (new SnakeHeadGameObject(ConvertToVector2(jsonBrick.Position))),// Snake
				6 => (new MegaBlobGameObject(ConvertToVector2(jsonBrick.Position))),// Mega Blob
				7 => (new AtomGameObject(ConvertToVector2(jsonBrick.Position), 1.0f)),// Atoms immobiles
				8 => (new BlobGameObject(ConvertToVector2(jsonBrick.Position), 1.0f)),// Blob
				9 => (new BonusGameObject(ConvertToVector2(jsonBrick.Position))),// Bonus
				_ => throw new ArgumentNullException(nameof(jsonBrick)),
			};
		}

		public IGameObject CreateBrick(ParsedBrick jsonBrick)
		{
			Vector2 destination = ConvertToVector2(jsonBrick.Position);
			return (DecorateEntrance(CreateNudeBrick(jsonBrick), new Vector2(destination.X, -300), destination));
		}

		public IGameObject DecorateEntrance(IGameObject gameObject, Vector2 origin, Vector2 destination)
		{
			float fallTtl = 2.0f; // Services.Instance.Get<IRandomService>().Next() * 1.1f + 0.4f;
			float delay = 0.0f; // Services.Instance.Get<IRandomService>().Next() * 0.3f + 0.2f;
			MaxTtl = Math.Max(MaxTtl, fallTtl + delay);
			return (gameObject is IBrickGameObject ? new BrickTweenMoveDecorator(gameObject as IBrickGameObject, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), origin, destination, fallTtl, delay) : new TweenMoveDecorator(gameObject, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), origin, destination, fallTtl, delay));
		}
	}
}
