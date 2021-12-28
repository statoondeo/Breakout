using System;
using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public class ColliderService : IColliderService
	{
		public ColliderService()
		{
		}

		public CollisionTestResult IsCollision(IBody body1, IBody body2)
		{
			CollisionTestResult result = null;

			// Si les 2 éléments sont , il n'y a pas d'effet
			if (body1.IsStatic && body2.IsStatic) return (null);

			if (body1 is IBoxBody)
			{
				if (body2 is IBoxBody)
				{
					result = IsBoxCollision(body1 as IBoxBody, body2 as IBoxBody);
				}
				else if (body2 is ICircleBody)
				{
					result = IsCircleBoxCollision(body2 as ICircleBody, body1 as IBoxBody);
				}
				else if (body2 is ICompositeIntersecBody)
				{
					result = IsCompositeIntersecBodyCollision(body2 as ICompositeIntersecBody, body1);
				}
			}
			else if (body1 is ICircleBody)
			{
				if (body2 is IBoxBody)
				{
					result = IsCircleBoxCollision(body1 as ICircleBody, body2 as IBoxBody);
				}
				else if (body2 is ICircleBody)
				{
					result = IsCircleCollision(body1 as ICircleBody, body2 as ICircleBody);
				}
				else if (body2 is ICompositeIntersecBody)
				{
					result = IsCompositeIntersecBodyCollision(body2 as ICompositeIntersecBody, body1);
				}
			}
			else if (body1 is ICompositeIntersecBody)
			{
				result = IsCompositeIntersecBodyCollision(body1 as ICompositeIntersecBody, body2, true);
			}

			return (result);
		}

		public void ResolveCollision(IBody body1, IBody body2, CollisionTestResult collisionResult)
		{
			// Résolution de la collision #1 : Supprimer les chevauchements
			// (Utilisation de la profondeur de collision)
			if (body1.IsStatic)
			{
				body2.Move(collisionResult.Normal * collisionResult.Depth);
			}
			else if (body2.IsStatic)
			{
				body1.Move(-collisionResult.Normal * collisionResult.Depth);
			}
			else
			{
				body1.Move(-collisionResult.Normal * collisionResult.Depth / 2.0f);
				body2.Move(collisionResult.Normal * collisionResult.Depth / 2.0f);
			}

			// Résolution de la collision #2 : Impact l'un sur l'autre
			Vector2 relativeVelocity = body2.Velocity - body1.Velocity;
			float relativeVelocityProduct = Vector2.Dot(relativeVelocity, collisionResult.Normal);
			if (relativeVelocityProduct > 0.0f) return;

			float j = -(1.0f + Math.Min(body1.Restitution, body2.Restitution)) * relativeVelocityProduct;
			//j /= body1.InvMass + body2.InvMass;
			//j /= 2;

			Vector2 impulse = j * collisionResult.Normal;

			//body1.Velocity -= impulse * body1.InvMass;
			//body2.Velocity += impulse * body2.InvMass;
			body1.Velocity -= impulse;
			body2.Velocity += impulse;
		}

		private CollisionTestResult IsCompositeIntersecBodyCollision(ICompositeIntersecBody compositeBody, IBody body, bool inverseNormal = false)
		{
			if (null != IsCollision(compositeBody.CollisionCheckerBody, body))
			{
				CollisionTestResult collisionResult = IsCollision(body, compositeBody.CollisionResolverBody);
				if (null == collisionResult) return (null);
				return (inverseNormal ? new CollisionTestResult(collisionResult.BodyA, collisionResult.BodyB, Vector2.Negate(collisionResult.Normal), collisionResult.Depth) : collisionResult);
			}
			return (null);
		}

		private CollisionTestResult IsCircleBoxCollision(ICircleBody circle, IBoxBody box)
		{
			float depth = float.MaxValue;
			float depthAxis;

			Vector2 va;
			Vector2 vb;
			Vector2 edge;
			Vector2 normal = Vector2.Zero;
			Vector2 newAxis;
			Vector2 projectionBox;
			Vector2 projectionCircle;

			// Traitement de la box
			Vector2[] boxVectors = box.Vectors;
			for (int i = 0; i < boxVectors.Length; i++)
			{
				va = boxVectors[i];
				vb = boxVectors[(i + 1) % boxVectors.Length];
				edge = vb - va;
				newAxis = Vector2.Normalize(new Vector2(-edge.Y, edge.X));

				projectionBox = ProjectVectorsToAxis(boxVectors, newAxis);
				projectionCircle = ProjectCircleToAxis(circle, newAxis);

				if ((projectionBox.X >= projectionCircle.Y) || (projectionCircle.X >= projectionBox.Y))
				{
					return (null);
				}

				depthAxis = Math.Min(projectionCircle.Y - projectionBox.X, projectionBox.Y - projectionCircle.X);
				if (depthAxis < depth)
				{
					depth = depthAxis;
					normal = newAxis;
				}
			}

			Vector2 nearestPoint = NearestPolygonPointToCenter(circle.Center, boxVectors);
			newAxis = Vector2.Normalize(nearestPoint - circle.Center);

			projectionBox = ProjectVectorsToAxis(boxVectors, newAxis);
			projectionCircle = ProjectCircleToAxis(circle, newAxis);

			if ((projectionBox.X >= projectionCircle.Y) || (projectionCircle.X >= projectionBox.Y))
			{
				return (null);
			}

			depthAxis = Math.Min(projectionCircle.Y - projectionBox.X, projectionBox.Y - projectionCircle.X);
			if (depthAxis < depth)
			{
				depth = depthAxis;
				normal = newAxis;
			}

			if (Vector2.Dot(GetCenter(boxVectors) - circle.Center, normal) > 0.0f)
			{
				normal = -normal;
			}

			return (new CollisionTestResult(circle, box, normal, depth));
		}

		private CollisionTestResult IsCircleCollision(ICircleBody circle1, ICircleBody circle2)
		{
			float distance = Vector2.Distance(circle1.Center, circle2.Center);
			float radiusSum = circle1.Radius + circle2.Radius;

			if (distance >= radiusSum)
			{
				return (null);
			}

			return (new CollisionTestResult(circle1, circle2, Vector2.Normalize(circle2.Center - circle1.Center), radiusSum - distance));
		}

		private CollisionTestResult IsBoxCollision(IBoxBody box1, IBoxBody box2)
		{
			Vector2 normal = Vector2.Zero;
			float depth = float.MaxValue;

			Vector2 va;
			Vector2 vb;
			Vector2 edge;
			Vector2 newAxis;
			Vector2 projectionBox1;
			Vector2 projectionBox2;
			float depthAxis;

			// Récupération des points constiutant les box
			Vector2[] box1Vectors = box1.Vectors;
			Vector2[] box2Vectors = box2.Vectors;

			// Traitement de la box1
			for (int i = 0; i < box1Vectors.Length; i++)
			{
				va = box1Vectors[i];
				vb = box1Vectors[(i + 1) % box1Vectors.Length];
				edge = vb - va;
				newAxis = Vector2.Normalize(new Vector2(-edge.Y, edge.X));

				projectionBox1 = ProjectVectorsToAxis(box1Vectors, newAxis);
				projectionBox2 = ProjectVectorsToAxis(box2Vectors, newAxis);

				if ((projectionBox1.X >= projectionBox2.Y) || (projectionBox2.X >= projectionBox1.Y))
				{
					// Si il y a un axe séparateur, alors pas de collision possible
					return (null);
				}

				depthAxis = Math.Min(projectionBox2.Y - projectionBox1.X, projectionBox1.Y - projectionBox2.X);
				if (depthAxis < depth)
				{
					// On conserve la direction et la profondeur de collision
					// pour résoudre la 1ere partie des collisions
					// à savoir repositionner les éléments pour qu'il n'y ait plus de collision
					// mais aussi pour savoir dans quelles directions vopnt repartir les éléments.
					depth = depthAxis;
					normal = newAxis;
				}
			}

			// Traitement de la box2
			for (int i = 0; i < box2Vectors.Length; i++)
			{
				va = box2Vectors[i];
				vb = box2Vectors[(i + 1) % box2Vectors.Length];
				edge = vb - va;
				newAxis = Vector2.Normalize(new Vector2(-edge.Y, edge.X));

				projectionBox1 = ProjectVectorsToAxis(box1Vectors, newAxis);
				projectionBox2 = ProjectVectorsToAxis(box2Vectors, newAxis);

				if ((projectionBox1.X >= projectionBox2.Y) || (projectionBox2.X >= projectionBox1.Y))
				{
					return (null);
				}

				depthAxis = Math.Min(projectionBox2.Y - projectionBox1.X, projectionBox1.Y - projectionBox2.X);
				if (depthAxis < depth)
				{
					depth = depthAxis;
					normal = newAxis;
				}
			}


			if (Vector2.Dot(GetCenter(box2Vectors) - GetCenter(box1Vectors), normal) < 0.0f)
			{
				normal = -normal;
			}

			return (new CollisionTestResult(box1, box2, normal, depth));
		}

		private Vector2 GetCenter(Vector2[] vectors)
		{
			Vector2 sum = Vector2.Zero;
			foreach (Vector2 vector in vectors)
			{
				sum += vector;
			}
			return (sum / vectors.Length);
		}

		private Vector2 ProjectVectorsToAxis(Vector2[] vectors, Vector2 axis)
		{
			float min = float.MaxValue;
			float max = float.MinValue;
			foreach (Vector2 vector in vectors)
			{
				float proj = Vector2.Dot(vector, axis);
				min = Math.Min(proj, min);
				max = Math.Max(proj, max);
			}
			return (new Vector2(min, max));
		}

		private Vector2 ProjectCircleToAxis(ICircleBody circle, Vector2 axis)
		{
			Vector2 weigthDirection = Vector2.Normalize(axis) * circle.Radius;
			float min = Vector2.Dot(circle.Center + weigthDirection, axis);
			float max = Vector2.Dot(circle.Center - weigthDirection, axis);

			if (min > max)
			{
				float t = min;
				min = max;
				max = t;
			}

			return (new Vector2(min, max));
		}

		private Vector2 NearestPolygonPointToCenter(Vector2 center, Vector2[] points)
		{
			int index = -1;
			float minDistance = float.MaxValue;
			for (int i = 0; i < points.Length; i++)
			{
				float distance = Vector2.Distance(center, points[i]);
				if (distance < minDistance)
				{
					index = i;
					minDistance = distance;
				}
			}
			return (points[index]);
		}
	}
}
