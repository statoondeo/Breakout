using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class GameObjectFactoryService : IGameObjectFactoryService
	{
		public float MaxTtl { get; protected set; }

		public GameObjectFactoryService()
		{
			ResetMaxTtl();
		}

		public void ResetMaxTtl()
		{
			MaxTtl = 0;
		}
		
		public Vector2 ConvertToVector2(ParsedVector2 vector2)
		{
			return (new Vector2(vector2.X, vector2.Y));
		}

		public IList<IGameObject> CreateLevel(ParsedLevel jsonLevel)
		{
			ResetMaxTtl();
			IList<IGameObject> gameObjectsCollection = new List<IGameObject>();

			// Chargement des backgrounds du level
			foreach (ParsedBackground background in jsonLevel.Backgrounds)
			{
				gameObjectsCollection.Add(CreateBackground(background));
			}

			// Chargement des briques du level
			foreach (ParsedBrick brick in jsonLevel.Bricks)
			{
				gameObjectsCollection.Add(CreateBrick(brick));
			}

			// Chargement des trigger du level
			foreach (ParsedTrigger trigger in jsonLevel.Triggers)
			{
				gameObjectsCollection.Add(CreateTrigger(trigger));
			}

			return (gameObjectsCollection);
		}

		public IGameObject CreateTrigger(ParsedTrigger trigger)
		{
			IGameObject gameObject;
			switch (trigger.Type)
			{
				case 0:
					gameObject = new BrainShield1Trigger();
					break;
				case 1:
					gameObject = null;
					break; 
				case 2:
					gameObject = new BrainWinTrigger();
					break;
				default:
					gameObject = null;
					break;
			}
			return (gameObject);
		}

		public IGameObject CreateBackground(ParsedBackground background)
		{
			IGameObject gameObject;
			switch (background.Type)
			{
				case 0:
					gameObject = new BackgroundGameObject(ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.Stars));
					break;
				case 1:
					gameObject = new RotatingBackgroundGameObject(ServiceLocator.Instance.Get<IAssetService>().GetTexture(TextureName.Gas3));
					break;
				default:
					gameObject = null;
					break;
			}
			return (gameObject);
		}

		public IGameObject CreateBrick(ParsedBrick jsonBrick)
		{
			float originX = -300;
			IGameObject gameObject;
			Vector2 origin, destination;
			switch (jsonBrick.Type)
			{
				case 0:
					gameObject = new WallGameObject(ConvertToVector2(jsonBrick.Position), ConvertToVector2(jsonBrick.Size));
					break;
				case 1:
					// Wobbler
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = SetEntranceDecoration(new WobblerBrickGameObject(Vector2.Zero, 1.0f), origin, destination);
					break;
				case 2:
					// Atoms
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = SetEntranceDecoration(new AtomBrickGameObject(Vector2.Zero, 1.0f, ConvertToVector2(jsonBrick.Center), jsonBrick.Radius, jsonBrick.Angle, jsonBrick.AngleSpeed), origin, destination);
					break;
				case 3:
					// Rocks
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = SetEntranceDecoration(new RockWallGameObject(Vector2.Zero), origin, destination);
					break;
				case 4:
					// Brain
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = SetEntranceDecoration(new BrainBrickGameObject(Vector2.Zero, 1.0f), origin, destination);
					break;
				case 5:
					// Shield
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = SetEntranceDecoration(new ShieldBrickGameObject(Vector2.Zero, 2.1f), origin, destination);
					break;
				default:
					gameObject = null;
					break;
			}
			return (gameObject);
		}

		public IGameObject SetEntranceDecoration(IGameObject gameObject, Vector2 origin, Vector2 destination)
		{
			float fallTtl = ServiceLocator.Instance.Get<IRandomService>().Next() * 1.1f + 0.4f;
			float delay = ServiceLocator.Instance.Get<IRandomService>().Next() * 0.3f + 0.2f;
			MaxTtl = Math.Max(MaxTtl, fallTtl + delay);
			return (new ElasticEntranceDecorator(gameObject, origin, destination, fallTtl, delay));
		}
	}
}
