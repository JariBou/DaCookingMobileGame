using System;
using System.Collections.Generic;
using System.Linq;
using _project.ScriptableObjects.Scripts;
using UnityEngine;

namespace _project.Scripts.Core
{
    public static class Utils
    {

        public static bool ListHasAllElements<T>(List<T> listToTest, List<T> listToTestFor)
        {
            foreach (T el in listToTest.Where(listToTestFor.Contains))
            {
                listToTestFor.Remove(el);
            }

            return listToTestFor.Count <= 0;
        }
        
        public static int Mod(float x, float m)
        {
            int q = (int)Math.Floor(x / m);
            float r = x - q * m;
            return (int)r;
        }
    }

    public static class Extensions
    {

        public static Vector3Int ClampCustom(this Vector3Int self, Vector3Int min, Vector3Int max)
        {
            self.x = Math.Clamp(self.x, min.x, max.x);
            self.y = Math.Clamp(self.y, min.y, max.y);
            self.z = Math.Clamp(self.z, min.z, max.z);
            return self;
        }
        
        public static bool IsInBounds(this Vector3Int self, Vector3Int min, Vector3Int max)
        {
            bool isInBounds = true;

            isInBounds &= self.x > min.x && self.x < max.x;
            isInBounds &= self.y > min.y && self.y < max.y;
            isInBounds &= self.z > min.z && self.z < max.z;
            
            return isInBounds;
        }
        
    }
}