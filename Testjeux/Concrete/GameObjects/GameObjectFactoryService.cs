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
					gameObject = new CommonLooseTrigger();
					break;
				case 2:
					gameObject = new BrainWinTrigger();
					break;
				case 3:
					gameObject = new BlobWinTrigger();
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
					gameObject = new ScrollingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture((TextureName)Enum.Parse(typeof(TextureName), background.Texture)), ConvertToVector2(background.Velocity), ConvertToVector2(background.Position));
					break;
				case 1:
					gameObject = new RotatingBackgroundGameObject(Services.Instance.Get<IAssetService>().GetTexture((TextureName)Enum.Parse(typeof(TextureName), background.Texture)), background.AngleSpeed);
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
					gameObject = DecorateEntrance(new WobblerBrickGameObject(Vector2.Zero, 1.0f), origin, destination);
					break;
				case 2:
					// Atoms
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new AtomBrickGameObject(Vector2.Zero, 1.0f, ConvertToVector2(jsonBrick.Center), jsonBrick.Radius, jsonBrick.Angle, jsonBrick.AngleSpeed), origin, destination);
					break;
				case 3:
					// Rocks
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new RockWallGameObject(Vector2.Zero), origin, destination);
					break;
				case 4:
					// Brain
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new BrainBrickGameObject(Vector2.Zero, 1.0f), origin, destination);
					break;
				case 5:
					// Shield
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, originX);
					gameObject = DecorateEntrance(new ShieldBrickGameObject(Vector2.Zero, 2.1f), origin, destination);
					break;
				case 6:
					// Mega Blob
					destination = ConvertToVector2(jsonBrick.Position);
					origin = new Vector2(destination.X, -1000.0f);
					gameObject = DecorateEntrance(new MegaBlobGameObject(Vector2.Zero), origin, destination);
					break;
				default:
					gameObject = null;
					break;
			}
			return (gameObject);
		}

		public IGameObject DecorateEntrance(IGameObject gameObject, Vector2 origin, Vector2 destination)
		{
			float fallTtl = Services.Instance.Get<IRandomService>().Next() * 1.1f + 0.4f;
			float delay = Services.Instance.Get<IRandomService>().Next() * 0.3f + 0.2f;
			MaxTtl = Math.Max(MaxTtl, fallTtl + delay);
			return (new TweenMoveDecorator(gameObject, Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintOut), origin, destination, fallTtl, delay));
		}
	}
}
