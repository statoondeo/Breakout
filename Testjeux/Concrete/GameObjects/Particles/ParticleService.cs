namespace GameNameSpace
{
	public class ParticleService
	{
		protected ObjectPool<ParticleGameObject> ObjectPool;

		public ParticleService(int capacity)
		{
			ObjectPool = new ObjectPool<ParticleGameObject>(capacity);
		}

		public ParticleGameObject GetParticle()
		{
			return (ObjectPool.Get());
		}
	}
}