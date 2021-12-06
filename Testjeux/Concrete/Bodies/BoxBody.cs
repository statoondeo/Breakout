using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class BoxBody : BaseBody, IBoxBody
	{
		public BoxBody(Vector2 position, Vector2 size, Vector2 velocity, float mass, float restitution, bool isStatic, IColliderCommand command)
			: base(position, velocity, mass, restitution, isStatic, command)
		{
			Size = size;

			InternalVectors = new Vector2[4];

			InternalVectors[0] = Vector2.Zero;
			InternalVectors[1] = new Vector2(size.X, 0);
			InternalVectors[2] = new Vector2(size.X, size.Y);
			InternalVectors[3] = new Vector2(0, size.Y);
		}

		public Vector2 Size { get; private set; }

		protected Vector2[] InternalVectors;

		public Vector2[] Vectors
		{
			get
			{
				Vector2[] vectors = new Vector2[InternalVectors.Length];
				for (int i = 0; i < InternalVectors.Length; i++)
				{
					vectors[i] = InternalVectors[i] + Position;
				}
				return (vectors);
			}
		}
	}
}
