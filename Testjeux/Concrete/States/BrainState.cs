using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public abstract class BrainState : IStateItem
	{
		public IList<IStateItem> Transitions { get; protected set; }
		public IStateContainer Container { get; protected set; }
		protected IRenderable Renderable;

		protected BrainState(IStateContainer container, IRenderable renderable)
		{
			Transitions = new List<IStateItem>();
			Container = container;
			Renderable = renderable;
		}

		public virtual void Enter() { }
		public virtual void Exit() { }

		public virtual void Update(GameTime gameTime)
		{
			Renderable.Update(gameTime);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			Renderable.Draw(spriteBatch);
		}
	}
}