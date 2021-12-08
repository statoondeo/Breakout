using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class RacketGameObject : BaseGameObject
	{
		protected IParticlesEmitter TrailParticlesEmitter;

		public Vector2 Size { get; protected set; }

		public RacketGameObject(Vector2 position, Vector2 size)
			: base()
		{
			Texture2D texture = ServiceLocator.Instance.Get<ShapeFactory>().CreateTexture(size.ToPoint(), Color.CornflowerBlue);
			Texture2D textureTrail = ServiceLocator.Instance.Get<ShapeFactory>().CreateTexture(size.ToPoint(), Color.CornflowerBlue);
			Size = size;
			Type = GameObjectType.RACKET;
			Movable = new RacketMouseMovable(this);
			Body = new RacketBody(position, size);
			Renderable = new TextureRenderable(this, texture);
			TrailParticlesEmitter = new RacketTrailParticlesEmitter(this, textureTrail, 5);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			TrailParticlesEmitter.Emit(gameTime);
		}
	}
}