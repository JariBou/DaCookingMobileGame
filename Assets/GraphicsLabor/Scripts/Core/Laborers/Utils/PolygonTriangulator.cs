using System;
using System.Collections.Generic;
using GraphicsLabor.Scripts.Core.Shapes;
using UnityEngine;

namespace GraphicsLabor.Scripts.Core.Laborers.Utils
{
    // Implementation inspired by the ear clipping algorithm
    public class PolygonTriangulator : MonoBehaviour
    {

        /// <summary>
        /// Returns a list of Triangles composing the polygon. !This algorithm is very naive and breaks at some shapes!
        /// </summary>
        /// <param name="polygon">The Polygon to "Triangulate"</param>
        /// <returns></returns>
        /// <exception cref="TimeoutException">If Algorithm takes more than 1s, throws the exception</exception>
        public static List<Triangle> TriangulatePolygon(Polygon polygon)
        {
            // Not Perfect but off to a good start
            float startTime = Time.realtimeSinceStartup;
            List<Vector2> vertices = new List<Vector2>(polygon.Points);
            List<Triangle> triangles = new List<Triangle>(polygon.Points.Count-2);

            int i = 0;
            while (vertices.Count != 2)
            {
                if (Time.realtimeSinceStartup - startTime >= 1)
                {
                    throw new TimeoutException("Triangulation of Polygon took more than 1s to complete. Polygon might be not valid and/or too complex.");
                }
                
                int prevIndex = i > 0 ? i - 1 : vertices.Count-1;
                int nextIndex = i < vertices.Count-1 ? i + 1 : 0;
                
                Vector2 prevVert = vertices[prevIndex];
                Vector2 vi = vertices[i];
                Vector2 nextVert = vertices[nextIndex];
                
                float signedAngle = Vector2.SignedAngle(prevVert - vi, nextVert - vi);
                bool isConvex = signedAngle is > 0 and < 180;
                
                if (isConvex)
                {
                    triangles.Add(new Triangle(prevVert, vi, nextVert, polygon.GetColor));
                    vertices.RemoveAt(i);
                }
                else if (Math.Abs(Math.Abs(signedAngle) - 180) < 0.001) // If 3 vertices next to each other are aligned
                {
                    vertices.RemoveAt(i);
                }
                else
                {
                    i++;
                }

                i %= vertices.Count;

            }

            return triangles;
        }
        
    }
}
