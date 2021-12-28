using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class CubeGameObject : BaseBrickGameObject
	{
		public CubeGameObject(Vector2 position)
			: base(position, 32, 1)
		{
			Body = new BrickBody(position, 32, new BrickColliderCommand(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.Cube), 15)));
			Renderable = new CubeRenderable(this, 1.0f);
		}

		public override void Damage()
		{
		}
	}
}