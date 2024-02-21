using System.Collections.Generic;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Core.Laborers;
using GraphicsLabor.Scripts.Core.Laborers.Utils;
using GraphicsLabor.Scripts.Core.Shapes;
using UnityEngine;

namespace GraphicsLabor.Samples.SampleScripts
{
    public class GraphicsTestScript : MonoBehaviour
    {
        [Space, Header("Quad")] 
        public bool _drawQuad;
        public Quad _quad;
        public Color _quadBorderColor;
        public LaborerDrawMode _quadLaborerDrawMode;
        
        [Space, Header("Cube")] 
        public bool _drawCube;
        public Cube _cube;
        public Color _cubeBorderColor;
        public LaborerDrawMode _cubeLaborerDrawMode;
        
        [Space, Header("Circle")]
        public bool _drawCircle;
        public Circle _circle;
        public Color _circleBorderColor;
        public LaborerDrawMode _circleLaborerDrawMode;

        [Space, Header("Triangle")]
        public bool _drawTriangle;
        public Triangle _triangle;
        public Color _triangleBorderColor;
        public LaborerDrawMode _triangleLaborerDrawMode;

        [Space, Header("Polygon"), ShowMessage("Move this GO's children to move the points of the polygon", MessageLevel.Info)]
        public bool _drawPolygon;
        public List<Transform> _polygonPoints;
        public Polygon _polygon;
        public Color _polygonBorderColor;
        public LaborerDrawMode _polygonLaborerDrawMode;

        void OnEnable()
        {
            GraphicLaborer.DrawCallback += OnDraw;
        }
        
        void OnDisable()
        {
            GraphicLaborer.DrawCallback -= OnDraw;
        }

        private void OnDraw()
        {
            if (_drawQuad)
            {
                Laborer2D.DrawQuad(_quad, _quadLaborerDrawMode, _quadBorderColor);
            }
            if (_drawCircle)
            {
                Laborer2D.DrawCircle(_circle, _circleLaborerDrawMode, _circleBorderColor);
            }
            if (_drawTriangle)
            {
                Laborer2D.DrawTriangle(_triangle, _triangleLaborerDrawMode, _triangleBorderColor);
            }
            if (_drawPolygon)
            {
                GatherPolygonPoints();
                Laborer2D.DrawPolygon(_polygon, _polygonLaborerDrawMode, _polygonBorderColor);
            }
            if (_drawCube)
            {
                Laborer3D.DrawCube(_cube, _cubeLaborerDrawMode, _cubeBorderColor); 
            }
        }
        
        private void OnValidate()
        {
            GatherPolygonPoints();
        }

        [Button]
        private void GatherPolygonPoints()
        {
            _polygon.ResetPoints(_polygonPoints.Count);
            foreach (Transform polygonPoint in _polygonPoints)
            {
                _polygon.Points.Add(polygonPoint.position);
            }
        }
    }
}
