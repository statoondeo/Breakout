using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface ISceneService : IService
	{
		IScene GetCurrent();
		IScene ChangeScene(SceneType scene);
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch);
	}
}

