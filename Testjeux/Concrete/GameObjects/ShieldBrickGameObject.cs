using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class ShieldBrickGameObject : BaseBrickGameObject
	{
		public static readonly float BodySizeFactor = 0.9f;
		public static readonly Vector2 TextureSize = new Vector2(122);

		public ShieldBrickGameObject(Vector2 position, float scale)
			: base(position, TextureSize.X * 0.5f * scale * BodySizeFactor, 0)
		{
			Vector2 offset = (TextureSize * BodySizeFactor - TextureSize);
			Body = new BumperBody(position, TextureSize.X * 0.5f * scale * BodySizeFactor, new ShieldColliderCommand(this));
			Renderable = new ShieldTextureRenderable(this, scale, offset);
		}

		public override void Damage()
		{
			base.Damage();
			Services.Instance.Get<IAssetService>().GetSound(SoundName.Explosion1).Play();
		}
	}
}