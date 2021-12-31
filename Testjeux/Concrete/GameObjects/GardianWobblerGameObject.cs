using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class GardianWobblerGameObject : BaseBrickGameObject
	{
		private readonly IRenderable SparkRenderable;
		private readonly IGameObject Halo1;
		private readonly IGameObject Halo2;

		public GardianWobblerGameObject(Vector2 position, float scale)
			: base(position, 32, 1)
		{
			Renderable = new WobblerRenderable(this, scale);
			SparkRenderable = new AnimatedTextureRenderable(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.BlueSpark), new Vector2(-43), new Point(100), 1.5f, new TextureAnimation(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1 }, 0.09f, true));

			position += new Vector2(32);
			Halo1 = new HaloGameObject(Color.CornflowerBlue, 0.2f, 1.5f);
			Halo1.Renderable.Alpha = 0.2f;
			Vector2 origin = new Vector2(position.X, -500);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(Halo1, origin, position));

			Halo2 = new HaloGameObject(Color.CornflowerBlue, -0.3f, 1f);
			Halo2.Renderable.Alpha = 0.4f;
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(Halo2, origin, position));
		}

		public override void Damage()
		{
			base.Damage();
			if (Health <= 0)
			{
				Services.Instance.Get<IAssetService>().GetSound(SoundName.Explosion1).Play();
				Status = Halo1.Status = Halo2.Status = GameObjectStatus.OUTDATED;
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			SparkRenderable.Update(gameTime);
			Halo1.Body.MoveTo(Body.Position + new Vector2(32));
			Halo2.Body.MoveTo(Body.Position + new Vector2(32));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			SparkRenderable.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}
	}
}