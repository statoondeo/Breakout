using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameNameSpace
{
	public sealed class BrainBrickGameObject : BaseBrickGameObject
	{
		public static readonly float BodySizeFactor = 0.65f;
		public static readonly Vector2 TextureSize = new Vector2(256);

		private readonly IRenderable HaloRenderable;
		public bool HaloActivated { get; set; }
		private readonly IGameObject ShieldGameObject;

		public BrainBrickGameObject(Vector2 position, float scale)
			: base(position, TextureSize.X * 0.5f * scale * BodySizeFactor, 6)
		{
			Vector2 offset = (TextureSize * BodySizeFactor - TextureSize) * 0.5f + new Vector2(-5, 20);
			Body = new BrickBody(position, TextureSize.X * 0.5f * scale * BodySizeFactor, new BrainColliderCommand(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 25)));
			Renderable = new BrainAnimatedTextureRenderable(this, scale, offset);
			HaloActivated = false;
			Texture2D texture = Services.Instance.Get<IAssetService>().GetTexture(TextureName.LaserGlow);
			HaloRenderable = new TextureRenderable(this, texture, 3.0f, 0.5f * (new Vector2(64) - 3.0f * (new Vector2(texture.Width, texture.Height))))
			{
				Alpha = 0.25f,
				ColorMask = Color.Red
			};

			Vector2 destination = new Vector2(522, 88);
			Vector2 origin = new Vector2(destination.X, -300);
			ShieldGameObject = new ShieldBrickGameObject(Vector2.Zero, 2.1f);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(ShieldGameObject, origin, destination));

			Services.Instance.Get<ISceneService>().RegisterGameObject(new BrainShield1Trigger());

			destination = new Vector2(168, 208);
			origin = new Vector2(destination.X, -300);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(new WobblerBrickGameObject(Vector2.Zero, 1.0f), origin, destination));

			destination = new Vector2(1048, 208);
			origin = new Vector2(destination.X, -300);
			Services.Instance.Get<ISceneService>().RegisterGameObject(Services.Instance.Get<IGameObjectFactoryService>().DecorateEntrance(new WobblerBrickGameObject(Vector2.Zero, 1.0f), origin, destination));

			Services.Instance.Get<ISceneService>().RegisterGameObject(new BrainWinTrigger());
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (HaloActivated)
			{
				HaloRenderable.Draw(spriteBatch);
			}
			base.Draw(spriteBatch);
		}
	}
}