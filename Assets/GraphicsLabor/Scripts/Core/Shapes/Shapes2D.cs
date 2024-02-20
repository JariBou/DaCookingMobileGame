using System;
using System.Collections.Generic;
using GraphicsLabor.Scripts.Core.Laborers;
using UnityEngine;

namespace GraphicsLabor.Scripts.Core.Shapes
{
    
    [Serializable]
    public class Quad : Shape
    {
        [SerializeField] private Vector2 _pointA;
        [SerializeField] private Vector2 _pointB;
        [SerializeField] private Vector2 _pointC;
        [SerializeField] private Vector2 _pointD;
        
        public Vector2 PointA => _pointA;
        public Vector2 PointB => _pointB;
        public Vector2 PointC => _pointC;
        public Vector2 PointD => _pointD;
        
        public Quad(Vector2 pointA, Vector2 pointB, Vector2 pointC, Vector2 pointD, Color color)
        {
            _pointA = pointA;
            _pointB = pointB;
            _pointC = pointC;
            _pointD = pointD;

            _color = color;
        }
        
        public Quad(Vector2 center, Vector2 size, Color color)
        {
            _pointA = center - GraphicLaborer.MaskVector2(size, GraphicLaborer.XMask) / 2 + GraphicLaborer.MaskVector2(size, GraphicLaborer.YMask) / 2; // Top left
            _pointB = center + GraphicLaborer.MaskVector2(size, GraphicLaborer.XMask) / 2 + GraphicLaborer.MaskVector2(size, GraphicLaborer.YMask) / 2; // Top Right
            _pointC = center + GraphicLaborer.MaskVector2(size, GraphicLaborer.XMask) / 2 - GraphicLaborer.MaskVector2(size, GraphicLaborer.YMask) / 2; // Bottom Right
            _pointD = center - GraphicLaborer.MaskVector2(size, GraphicLaborer.XMask) / 2 - GraphicLaborer.MaskVector2(size, GraphicLaborer.YMask) / 2; // Bottom Left
            
            _color = color;
        }

        private Quad(Quad quad)
        {
            _pointA = quad.PointA;
            _pointB = quad.PointB;
            _pointC = quad.PointC;
            _pointD = quad.PointD;

            _color = quad.GetColor;
        }
        
        private Quad(Quad quad, Color color)
        {
            _pointA = quad.PointA;
            _pointB = quad.PointB;
            _pointC = quad.PointC;
            _pointD = quad.PointD;

            _color = color;
        }

        public Quad CopyWithColor(Color color)
        {
            return new Quad(this, color);
        }
    }
    
    [Serializable]
    public class Circle : Shape
    {
        [SerializeField] private Vector2 _center;
        [SerializeField, Min(0.001f)] private float _radius;
        [SerializeField, Range(1, 100)] private int _precision;

        public const int PointsPerPrecision = 5;
        
        public Vector2 Center => _center;
        public float Radius => _radius;
        public int Precision => _precision;

        public Circle(Vector2 center, float radius, Color color, int precision = 9)
        {
            _center = center;
            _radius = radius;
            _color = color;
            _precision = precision;
        }
        
        private Circle(Circle circle, Color color)
        {
            _center = circle.Center;
            _radius = circle.Radius;
            _precision = circle.Precision;

            _color = color;
        }

        public Circle CopyWithColor(Color color)
        {
            return new Circle(this, color);
        }
    }
    
    [Serializable]
    public class Polygon : Shape
    {
        [SerializeField] private List<Vector2> _points;
        
        public List<Vector2> Points => _points;

        public Polygon(List<Vector2> points, Color color)
        {
            _points = points;
            _color = color;
        }
        
        private Polygon(Polygon polygon, Color color)
        {
            _points = polygon.Points;

            _color = color;
        }
        
        public Polygon CopyWithColor(Color color)
        {
            return new Polygon(this, color);
        }

        public void ResetPoints(int length)
        {
            _points = new List<Vector2>(length);
        }
    }
    
    [Serializable]
    public class Triangle : Shape
    {
        [SerializeField] private Vector2 _pointA;
        [SerializeField] private Vector2 _pointB;
        [SerializeField] private Vector2 _pointC;
        
        public Vector2 PointA => _pointA;
        public Vector2 PointB => _pointB;
        public Vector2 PointC => _pointC;
        
        public Triangle(Vector2 pointA, Vector2 pointB, Vector2 pointC, Color color)
        {
            _pointA = pointA;
            _pointB = pointB;
            _pointC = pointC;

            _color = color;
        }

        private Triangle(Triangle triangle, Color color)
        {
            _pointA = triangle.PointA;
            _pointB = triangle.PointB;
            _pointC = triangle.PointC;

            _color = color;
        }

        public Triangle CopyWithColor(Color color)
        {
            return new Triangle(this, color);
        }
    }
}
