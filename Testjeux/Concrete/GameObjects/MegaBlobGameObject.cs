using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class MegaBlobGameObject : BaseGameObject
	{
		public MegaBlobGameObject(Vector2 position)
			: base()
		{
			Type = GameObjectType.NONE;

			IGameObject halo = Services.Instance.Get<ISceneService>().RegisterGameObject(new HaloGameObject(new Color(30, 255, 12), 0.1f, 1.5f));
			halo.Renderable.Alpha = 0.4f;
			halo.Body.MoveTo(new Vector2(640, 266));

			halo = Services.Instance.Get<ISceneService>().RegisterGameObject(new HaloGameObject(new Color(45, 255, 12), -0.2f, 1.0f));
			halo.Renderable.Alpha = 0.6f;
			halo.Body.MoveTo(new Vector2(640, 266));

			Body = new InvisibleBody(position);

			IGameObjectFactoryService factoryService = Services.Instance.Get<IGameObjectFactoryService>();
			factoryService.ResetTtl();
			Point screen = Services.Instance.Get<IScreenService>().GetScreenSize();
			Vector2 centerScreen = new Vector2(screen.X * 0.5f, screen.Y / 3.0f);
			IRandomService rand = Services.Instance.Get<IRandomService>();
			Vector2 originOrigin, origin, destination;
			ITweening tween = Services.Instance.Get<ITweeningService>().Get(TweeningName.QuintInOutTweening);

			origin = new Vector2(rand.Next() * 1152.0f + 64.0f, rand.Next() * 636.0f + 64.0f);
			originOrigin = new Vector2(origin.X, -300);
			destination = centerScreen - new Vector2(41);
			SortedList<float, IGameObject> sortedList = new SortedList<float, IGameObject>
			{
				{ rand.Next(), factoryService.DecorateEntrance(new TweenMoveDecorator(new ForkBlobBrickGameObject(originOrigin, 1.3f), tween, origin, destination, rand.Next() * 2.0f + 2.0f, 0.0f), originOrigin, origin)  }
			};

			float angle = 0;
			int number = 7;
			float angleStep = (float)Math.PI * 2.0f / number;
			for (int i = 0; i < number; i++)
			{
				origin = new Vector2(rand.Next() * 1152.0f + 64.0f, rand.Next() * 636.0f + 64.0f);
				originOrigin = new Vector2(origin.X, -300);
				destination = centerScreen - new Vector2(41) + new Vector2((float)Math.Cos(angle + i * angleStep) * 48.0f, (float)Math.Sin(angle + i * angleStep) * 48.0f);
				sortedList.Add(rand.Next(), factoryService.DecorateEntrance(new TweenMoveDecorator(new ForkBlobBrickGameObject(originOrigin, 1.3f), tween, origin, destination, rand.Next() * 4.0f + 2.0f, 0.0f), originOrigin, origin));
			}

			angle = 0;
			number = 15;
			angleStep = (float)Math.PI * 2.0f / number;
			for (int i = 0; i < number; i++)
			{
				origin = new Vector2(rand.Next() * 1152.0f + 64.0f, rand.Next() * 636.0f + 64.0f);
				originOrigin = new Vector2(origin.X, -300);
				destination = centerScreen - new Vector2(29) + new Vector2((float)Math.Cos(angle + i * angleStep) * 72.0f, (float)Math.Sin(angle + i * angleStep) * 72.0f);
				sortedList.Add(rand.Next(), factoryService.DecorateEntrance(new TweenMoveDecorator(new ForkBlobBrickGameObject(originOrigin, 0.9f), tween, origin, destination, rand.Next() * 6.0f + 2.0f, 0.0f), originOrigin, origin));
			}

			Services.Instance.Get<ISceneService>().RegisterGameObjects(sortedList.Values);

			Services.Instance.Get<ISceneService>().RegisterGameObject(new BlobWinTrigger());
		}
	}
}
