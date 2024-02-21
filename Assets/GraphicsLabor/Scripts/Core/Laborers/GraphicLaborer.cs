using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace GraphicsLabor.Scripts.Core.Laborers
{
    /// <summary>
    /// Usage: Attach a Laborer2D or Laborer3D to a GameObject on your scene that
    /// isn't destroyed for as long as you need to draw on screen 
    /// </summary>
    public abstract class GraphicLaborer : MonoBehaviour
    {
        private static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");
        private static readonly int DstBlend = Shader.PropertyToID("_DstBlend");
        private static readonly int Cull = Shader.PropertyToID("_Cull");
        private static readonly int ZWrite = Shader.PropertyToID("_ZWrite");
        
        public static readonly Vector3 XMask = new(1, 0, 0);
        public static readonly Vector3 YMask = new(0, 1, 0);
        public static readonly Vector3 ZMask = new(0, 0, 1);
        
        protected static readonly Color BaseBorderColor = Color.black;
        /// <summary>
        /// Subscribe to this event with a function calling Laborer's DrawingFunctions to draw on screen
        /// </summary>
        public static event Action DrawCallback;
        protected static GraphicLaborer Instance { get; private set; }
        internal Material RenderMaterial;


        protected static void CreateLineMaterial()
        {
            if (Instance.RenderMaterial) return;
            
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            Instance.RenderMaterial = new Material(shader)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            // Turn on alpha blending
            Instance.RenderMaterial.SetInt(SrcBlend, (int)BlendMode.SrcAlpha);
            Instance.RenderMaterial.SetInt(DstBlend, (int)BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            Instance.RenderMaterial.SetInt(Cull, (int)CullMode.Off);
            // Turn off depth writes
            Instance.RenderMaterial.SetInt(ZWrite, 0);
        }
        
        public static Vector2 MaskVector2(Vector2 a, Vector2 mask)
        {
            return new Vector2(a.x * mask.x, a.y * mask.y);
        }
        
        public static Vector3 MaskVector3(Vector3 a, Vector3 mask)
        {
            return new Vector3(a.x * mask.x, a.y * mask.y, a.z * mask.z);
        }
        
        #region Unity Setup

        private void Awake()
        {
            Instance ??= this;
        }
        
        private void OnEnable()
        {
        #if USING_URP
            RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
        #else
                Camera.onPostRender += PostRender;
        #endif
        }
        
        private void OnDisable()
        {
        #if USING_URP
            RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
        #else
                Camera.onPostRender -= PostRender;
        #endif
        }

        #if USING_URP
        private static void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera locCamera)
        {
            OnDrawCallback();
        }
        #else
        // This wont work because PostRender is only called for game Objects on Camera.
        // need to use event, will try it later
        private static void PostRender(Camera cam)
        {
            OnDrawCallback();
        }
        #endif
        
        private static void OnDrawCallback()
        {
            DrawCallback?.Invoke();
        }

        #endregion
        
        // Lines are already 2D and 3D
        #region Lines
        
        /// <summary>
        ///  Automatically ignores the list if length not even
        /// </summary>
        /// <param name="points"></param>
        /// <param name="color"></param>
        public static void DrawLines(IEnumerable<Vector3> points, Color color)
        {
            CreateLineMaterial();
            
            GL.PushMatrix();
            
            GL.Begin(GL.LINES);
            Instance.RenderMaterial.SetPass(0);
            
            GL.Color(color);
            IEnumerable<Vector3> enumerable = points as Vector3[] ?? points.ToArray();
            for (int i = 0; i < enumerable.Count()/2; i++)
            {
                GL.Vertex(enumerable.ElementAt(i));
            }
            GL.End();
            GL.PopMatrix();
        }
        
        /// <summary>
        ///  Automatically trims the list if length not even
        /// </summary>
        /// <param name="points"></param>
        /// <param name="colors">Colors of each point, length should be equal or greater than length of points</param>
        public static void DrawLines(IEnumerable<Vector3> points, IEnumerable<Color> colors)
        {
            // TODO: Are Arrays better than Lists?
            Vector3[] pointsArray = points as Vector3[] ?? points.ToArray();
            int numberOfLines = pointsArray.Length - pointsArray.Length % 2;

            Color[] colorsArray = colors as Color[] ?? colors.ToArray();
            if (colorsArray.Length < numberOfLines)
                throw new ArgumentException($"Argument colors of size {colorsArray.Length}, expected {numberOfLines}");
            
            CreateLineMaterial();
            
            GL.PushMatrix();
            
            GL.Begin(GL.LINES);
            Instance.RenderMaterial.SetPass(0);
            
            for (int i = 0; i < numberOfLines; i++)
            {
                GL.Color(colorsArray.ElementAt(i));
                GL.Vertex(pointsArray[i]);
            }
            
            GL.End();
            GL.PopMatrix();
        }
        
        /// <summary>
        ///  
        /// </summary>
        /// <param name="points"></param>
        /// <param name="color"></param>
        public static void DrawBrokenLine(IEnumerable<Vector3> points, Color color)
        {
            CreateLineMaterial();
            
            GL.PushMatrix();
            
            GL.Begin(GL.LINE_STRIP);
            Instance.RenderMaterial.SetPass(0);
            
            GL.Color(color);
            foreach (Vector3 point in points)
            {
                GL.Vertex(point);
            }
            GL.End();
            GL.PopMatrix();
        }
        
        public static void DrawBrokenLine(IEnumerable<Vector2> points, Color color)
        {
            CreateLineMaterial();
            
            GL.PushMatrix();
            
            GL.Begin(GL.LINE_STRIP);
            Instance.RenderMaterial.SetPass(0);
            
            GL.Color(color);
            foreach (Vector3 point in points)
            {
                GL.Vertex(point);
            }
            GL.End();
            GL.PopMatrix();
        }
        
        /// <summary>
        ///  
        /// </summary>
        /// <param name="points"></param>
        /// <param name="colors">Colors of each point, length should be equal or greater than length of points</param>
        public static void DrawBrokenLine(List<Vector3> points, List<Color> colors)
        {
            if (colors.Count < points.Count)
                throw new ArgumentException($"Argument colors of size {colors.Count}, expected {points.Count}");
            
            CreateLineMaterial();
            
            GL.PushMatrix();
            
            GL.Begin(GL.LINE_STRIP);
            Instance.RenderMaterial.SetPass(0);
            
            for (int i = 0; i < points.Count; i++)
            {
                GL.Color(colors[i]);
                GL.Vertex(points[i]);
            }
            
            GL.End();
            GL.PopMatrix();
        }

        public static void DrawLine(Vector3 a, Vector3 b, Color color)
        {
            CreateLineMaterial();
            
            GL.PushMatrix();
            
            GL.Begin(GL.LINES);
            Instance.RenderMaterial.SetPass(0);
            
            GL.Color(color);
            GL.Vertex(a);
            GL.Vertex(b);
            
            GL.End();
            GL.PopMatrix();
        }
        
        public static void DrawLine(Vector3 a, Vector3 b, Color colorA, Color colorB)
        {
            CreateLineMaterial();
            
            GL.PushMatrix();
            
            GL.Begin(GL.LINES);
            Instance.RenderMaterial.SetPass(0);
            
            GL.Color(colorA);
            GL.Vertex(a);
            GL.Color(colorB);
            GL.Vertex(b);
            
            GL.End();
            GL.PopMatrix();
        }
        
        #endregion
    }
}