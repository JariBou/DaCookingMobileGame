using System;
using UnityEngine;

namespace GraphicsLabor.Scripts.Core.Shapes
{
    [Serializable]
    public class Shape
    {
        [SerializeField] protected Color _color;
        public Color GetColor => _color;

        public void ChangeColor(Color color)
        {
            _color = color;
        }
        
        // sadly it doesn't seem to be possible...
        // public T Copy<T>() where T : new()
        // {
        //     if (typeof(T) == typeof(Cube) && GetType() == typeof(Cube))
        //     {
        //         if (this is Cube self) return new T(self.PointA, self.PointB, self.PointC, self.PointD, self.PointE, self.PointF, self.PointG, self.PointH, self._color);
        //     }
        //
        //     throw new NotImplementedException();
        // }
    }
}
