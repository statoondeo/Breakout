using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public interface IGameObjectFactoryService : IService
	{
		float MaxTtl { get; }
		void ResetTtl();
		IGameObject CreateBrick(ParsedBrick jsonBrick);
		IList<IGameObject> CreateLevel(ParsedLevel jsonBricksList);
		Song CreateMusic(ParsedMusic music);
		IGameObject DecorateEntrance(IGameObject gameObject, Vector2 origin, Vector2 destination);
	}
}
