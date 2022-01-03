using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace GameNameSpace
{
	public interface IGameObjectFactoryService : IService
	{
		float MaxTtl { get; }
		Vector2 ConvertToVector2(ParsedVector2 vector2);
		IGameObject CreateBackground(ParsedBackground background);
		IGameObject CreateNudeBrick(ParsedBrick jsonBrick);
		IGameObject CreateBrick(ParsedBrick jsonBrick);
		IList<IGameObject> CreateLevel(ParsedLevel jsonLevel);
		Song CreateMusic(ParsedMusic music);
		IGameObject CreateTrigger(ParsedTrigger trigger);
		IGameObject DecorateEntrance(IGameObject gameObject, Vector2 origin, Vector2 destination);
		void ResetTtl();
	}
}
