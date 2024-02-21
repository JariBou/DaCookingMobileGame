using System;
using GraphicsLabor.Scripts.Core.Laborers.Utils;
using GraphicsLabor.Scripts.Core.Shapes;
using UnityEngine;

namespace GraphicsLabor.Scripts.Core.Laborers
{
    [AddComponentMenu("GraphicsLabor/Graphics/Laborer 3D")]
    public class Laborer3D : GraphicLaborer
    {
        #region Faces

        public static void DrawFace(Face face, LaborerDrawMode laborerDrawMode = LaborerDrawMode.Wired, Color borderColor = default)
        {
            CreateLineMaterial();
            
            int glDrawMode;
            switch (laborerDrawMode)
            {
                case LaborerDrawMode.Wired:
                    glDrawMode = GL.LINE_STRIP;
                    break;
                case LaborerDrawMode.Filled:
                    glDrawMode = GL.QUADS;
                    break;
                case LaborerDrawMode.FilledWithBorders:
                    glDrawMode = GL.QUADS;
                    if (borderColor == default) borderColor = BaseBorderColor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(laborerDrawMode), laborerDrawMode, null);
            }
            
            GL.PushMatrix();
            
            GL.Begin(glDrawMode);
            Instance.RenderMaterial.SetPass(0);
            
            GL.Color(face.GetColor);

            GL.Vertex(face.PointA);
            GL.Vertex(face.PointB);
            GL.Vertex(face.PointC);
            GL.Vertex(face.PointD);
            if (laborerDrawMode == LaborerDrawMode.Wired)
            {
                GL.Vertex(face.PointA);
            }
            
            GL.End();
            
            if (laborerDrawMode == LaborerDrawMode.FilledWithBorders)
            {
                DrawFace(face.CopyWithColor(borderColor));
            }
            
            GL.PopMatrix();
        }

        #endregion
        
        #region Cubes

        /// <summary>
        /// Draws a Cube on screen
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="laborerDrawMode">FilledWithBorders mode is not perfect for Method DrawCube</param>
        /// <param name="borderColor"></param>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void DrawCube(Cube cube, LaborerDrawMode laborerDrawMode = LaborerDrawMode.Wired, Color borderColor = default)
        {
            switch (laborerDrawMode)
            {
                case LaborerDrawMode.Filled:
                    if (cube.Faces.Count == 0) cube.CreateFaces();
                    foreach (Face cubeFace in cube.Faces)
                    {
                        DrawFace(cubeFace, LaborerDrawMode.Filled);
                    }
                    break;
                
                case LaborerDrawMode.Wired:
                    if (cube.Faces.Count == 0) cube.CreateFaces();
                    foreach (Face cubeFace in cube.Faces)
                    {
                        DrawFace(cubeFace);
                    }
                    break;
                
                case LaborerDrawMode.FilledWithBorders:
                    // Debug.Log("DrawMode FilledWithBorders is not perfect for Method DrawCube");
                    if (cube.Faces.Count == 0) cube.CreateFaces();
                    if (borderColor == default) borderColor = BaseBorderColor;
                    foreach (Face cubeFace in cube.Faces)
                    {
                        DrawFace(cubeFace, LaborerDrawMode.FilledWithBorders, borderColor);
                    }
                    break;

                case LaborerDrawMode.InternalWired:
                    throw new ArgumentException("InternalWiredMode is not supported for Method DrawCube");
                default:
                    throw new ArgumentOutOfRangeException(nameof(laborerDrawMode), laborerDrawMode, null);
            }
        }

        #endregion
    }
}
