using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public interface IScene
	{
		void Load();
		void UnLoad(ICommand commandWhenUnloaded);
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch);
		void Win();
		void Loose();
		IGameObject RegisterGameObject(IGameObject gameObject);
		IGameObject UnRegisterGameObject(IGameObject gameObject);
		IGameObject GetObject(Func<IGameObject, bool> predicate);
		IList<IGameObject> GetObjects(Func<IGameObject, bool> predicate);
	}
}