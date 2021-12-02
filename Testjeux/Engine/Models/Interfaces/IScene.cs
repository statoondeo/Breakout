using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IScene
	{
		IList<IGameObject> GameObjectsCollection { get; }
		void Load();
		void UnLoad(ICommand commandWhenUnloaded);
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch);
		IGameObject RegisterGameObject(IGameObject gameObject);
		IGameObject UnRegisterGameObject(IGameObject gameObject);
	}
}