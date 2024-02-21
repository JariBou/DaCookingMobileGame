using System;
using System.Collections.Generic;
using GraphicsLabor.Scripts.Core.Laborers;
using UnityEngine;

namespace GraphicsLabor.Scripts.Core.Shapes
{
    [Serializable]
    public class Cube : Shape
    {
        [SerializeField] private Vector3 _pointA;
        [SerializeField] private Vector3 _pointB;
        [SerializeField] private Vector3 _pointC;
        [SerializeField] private Vector3 _pointD;

        [SerializeField] private Vector3 _pointE;
        [SerializeField] private Vector3 _pointF;
        [SerializeField] private Vector3 _pointG;
        [SerializeField] private Vector3 _pointH;
        
        [SerializeField] private List<Face> _faces;
        
        public List<Vector3> TopSquare => new() { _pointA, _pointB, _pointC, _pointD };
        public List<Vector3> BottomSquare => new() { _pointE, _pointF, _pointG, _pointH };

        public Vector3 PointA => _pointA;
        public Vector3 PointB => _pointB;
        public Vector3 PointC => _pointC;
        public Vector3 PointD => _pointD;
        public Vector3 PointE => _pointE;
        public Vector3 PointF => _pointF;
        public Vector3 PointG => _pointG;
        public Vector3 PointH => _pointH;
        
        public List<Face> Faces => _faces;


        public Cube(Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 pointD, Vector3 pointE, Vector3 pointF,
            Vector3 pointG, Vector3 pointH, Color color)
        {
            _pointA = pointA;
            _pointB = pointB;
            _pointC = pointC;
            _pointD = pointD;
            _pointE = pointE;
            _pointF = pointF;
            _pointG = pointG;
            _pointH = pointH;

            _color = color;
            CreateFaces();
        }

        public Cube(Vector3 center, Vector3 size, Color color)
        {
            _pointA = center - GraphicLaborer.MaskVector3(size, GraphicLaborer.XMask) / 2 +
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.YMask) / 2 +
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.ZMask); // Top left
            _pointB = center + GraphicLaborer.MaskVector3(size, GraphicLaborer.XMask) / 2 +
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.YMask) / 2 +
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.ZMask); // Top Right
            _pointC = center + GraphicLaborer.MaskVector3(size, GraphicLaborer.XMask) / 2 -
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.YMask) / 2 +
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.ZMask); // Bottom Right
            _pointD = center - GraphicLaborer.MaskVector3(size, GraphicLaborer.XMask) / 2 -
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.YMask) / 2 +
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.ZMask); // Bottom Left

            _pointE = center - GraphicLaborer.MaskVector3(size, GraphicLaborer.XMask) / 2 +
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.YMask) / 2 -
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.ZMask); // Top left
            _pointF = center + GraphicLaborer.MaskVector3(size, GraphicLaborer.XMask) / 2 +
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.YMask) / 2 -
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.ZMask); // Top Right
            _pointG = center + GraphicLaborer.MaskVector3(size, GraphicLaborer.XMask) / 2 -
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.YMask) / 2 -
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.ZMask); // Bottom Right
            _pointH = center - GraphicLaborer.MaskVector3(size, GraphicLaborer.XMask) / 2 -
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.YMask) / 2 -
                      GraphicLaborer.MaskVector3(size, GraphicLaborer.ZMask); // Bottom Left

            _color = color;
            CreateFaces();
        }

        private Cube(Cube cube, Color color)
        {
            _pointA = cube.PointA;
            _pointB = cube.PointB;
            _pointC = cube.PointC;
            _pointD = cube.PointD;
            _pointE = cube.PointE;
            _pointF = cube.PointF;
            _pointG = cube.PointG;
            _pointH = cube.PointH;
            
            _color = color;
            CreateFaces();
        }

        public void CreateFaces()
        {
            _faces = new List<Face>(6)
            {
                new(_pointA, _pointB, _pointC, _pointD, _color),
                new(_pointE, _pointF, _pointG, _pointH, _color)
            };

            for (int i = 0; i < 4; i++)
            {
                _faces.Add(new Face(TopSquare[i], TopSquare[(i + 1) % 4], BottomSquare[(i + 1) % 4], BottomSquare[i], _color));
            }
        }

        public Cube CopyWithColor(Color color)
        {
            return new Cube(this, color);
        }
    }
    
    [Serializable]
    public class Face : Shape
    {
        [SerializeField] private Vector3 _pointA;
        [SerializeField] private Vector3 _pointB;
        [SerializeField] private Vector3 _pointC;
        [SerializeField] private Vector3 _pointD;
        
        public Vector3 PointA => _pointA;
        public Vector3 PointB => _pointB;
        public Vector3 PointC => _pointC;
        public Vector3 PointD => _pointD;
        
        public Face(Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 pointD, Color color)
        {
            _pointA = pointA;
            _pointB = pointB;
            _pointC = pointC;
            _pointD = pointD;

            _color = color;
        }

        private Face(Face face, Color color)
        {
            _pointA = face.PointA;
            _pointB = face.PointB;
            _pointC = face.PointC;
            _pointD = face.PointD;

            _color = color;
        }

        public Face CopyWithColor(Color color)
        {
            return new Face(this, color);
        }
    }
}

