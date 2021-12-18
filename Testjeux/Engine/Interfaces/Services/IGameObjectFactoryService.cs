using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public interface IGameObjectFactoryService : IService
	{
		float MaxTtl { get; }
		void ResetTtl();
		IGameObject CreateBrick(ParsedBrick jsonBrick);
		IList<IGameObject> CreateLevel(ParsedLevel jsonBricksList);
		IGameObject DecorateEntrance(IGameObject gameObject, Vector2 origin, Vector2 destination);
	}
}
