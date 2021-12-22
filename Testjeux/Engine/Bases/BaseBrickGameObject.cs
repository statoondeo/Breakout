using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public abstract class BaseBrickGameObject : BaseGameObject, IBrickGameObject
	{
		protected BaseBrickGameObject(Vector2 position, float radius, int health) 
			: base() 
		{
			Type = GameObjectType.BRICK;
			Body = new BrickBody(position, radius, new BrickColliderCommand(this, new BrickExplosionParticlesEmitter(this, Services.Instance.Get<IAssetService>().GetTexture(TextureName.RedSpark), 15)));
			Health = health;
			MaxDamageTtl = 0.1f;
		}

		public int Health { get; set; }
		protected bool IsDamaged;
		protected float DamageTtl;
		protected float MaxDamageTtl;

		public virtual void Damage()
		{
			Health--;
			IsDamaged = true;
			DamageTtl = MaxDamageTtl;
			Renderable.ColorMask = Color.Red;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (IsDamaged)
			{
				DamageTtl -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (DamageTtl < 0.0f)
				{
					IsDamaged = false;
					Renderable.ColorMask = Color.White;
				}
			}
		}

	}
}