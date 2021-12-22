using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class ForkBlobBrickGameObject : BaseBrickGameObject
	{
		public ForkBlobBrickGameObject(Vector2 position, float scale)
			: base(position, 32, 1)
		{
			Body = new BrickBody(position, 64 * 0.5f * scale * 1.0f, new BrickColliderCommand(this, new CompositeParticulesEmitter(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.GreenSpark), 25), new BlobExplosionParticlesEmitter(this))));
			Renderable = new BlobAnimatedTextureRenderable(this, scale);
		}
	}
}