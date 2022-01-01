﻿using System;
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

		public IGameObject CreateBrick(ParsedBrick jsonBrick)
		{
			float originX = -300;
			Vector2 origin, destination;
			IGameObject gameObject;
			switch (jsonBrick.Type)
			{
				case 0:
					gameObject = new WallGameObject(ConvertToVector2(jsonBrick.Position), ConvertToVector2(jsonBrick.Size));
					break;
				case 1:
					// Wobbler
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new WobblerGameObject(Vector2.Zero, 1.0f), origin, destination);
					break;
				case 2:
					// Atoms rotatifs
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new AtomGameObject(Vector2.Zero, 1.0f, ConvertToVector2(jsonBrick.Center), jsonBrick.Radius, jsonBrick.Angle, jsonBrick.AngleSpeed), origin, destination);
					break;
				case 3:
					// Cube
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new CubeGameObject(Vector2.Zero), origin, destination);
					break;
				case 4:
					// Brain
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new BrainGameObject(Vector2.Zero, 1.0f), origin, destination);
					break;
				case 5:
					// Snake
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new SnakeHeadGameObject(new Vector2(1600, 32)), origin, destination);
					break;
				case 6:
					// Mega Blob
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, -1000.0f);
					gameObject = DecorateEntrance(new MegaBlobGameObject(Vector2.Zero), origin, destination);
					break;
				case 7:
					// Atoms immobiles
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new AtomGameObject(Vector2.Zero, 1.0f), origin, destination);
					break;
				case 8:
					// Blob
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new BlobGameObject(Vector2.Zero, 1.0f), origin, destination);
					break;
				case 9:
					// Bonus
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new BonusGameObject(destination), origin, destination);
					break;

				default:
					throw new ArgumentNullException(nameof(jsonBrick));
					break;
			}
			return (gameObject);
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
