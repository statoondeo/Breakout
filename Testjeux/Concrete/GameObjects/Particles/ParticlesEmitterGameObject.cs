using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public class ParticlesEmitterGameObject : BaseGameObject
	{
		protected float CurrentTtl;
		protected float MaxTtl;
		protected IGameObject GameObject;
		protected Texture2D Texture;

		public ParticlesEmitterGameObject(IGameObject gameObject)
			: base()
		{
			GameObject = gameObject;
			CurrentTtl = 0.5f;
			Texture = ServiceLocator.Instance.Get<ShapeFactory>().CreateTexture(new Point(20), Color.BlueViolet);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			CurrentTtl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (CurrentTtl < 0)
			{
				CurrentTtl = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * 0.025 + 0.025);
				float angleSpeed = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * 2 * Math.PI);
				float ttl = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() + 0.5);
				float scale = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * 0.8 + 0.2);
				float particleSpeed = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * 100);
				float particleAngle = (float)(ServiceLocator.Instance.Get<Random>().NextDouble() * 2 * Math.PI);
				ServiceLocator.Instance.Get<GameState>().CurrentScene.GeneratedGameObjectsCollection.Add(new ParticlesGameObject(Texture, GameObject.Body.Position, new Vector2(particleSpeed * (float)Math.Cos(particleAngle), particleSpeed * (float)Math.Sin(particleAngle)), scale, ttl, angleSpeed));
			}
		}
	}
}