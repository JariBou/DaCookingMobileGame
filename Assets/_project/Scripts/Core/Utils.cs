using System;
using System.Collections.Generic;
using System.Linq;
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

            return !listToTestFor.Any();
        }
    }

    public static class Extensions
    {

        public static Vector3 Clamp(this Vector3 self, Vector3 min, Vector3 max)
        {
            self.x = Math.Clamp(self.x, min.x, max.x);
            self.y = Math.Clamp(self.y, min.y, max.y);
            self.z = Math.Clamp(self.z, min.z, max.z);
            return self;
        }
        
        public static bool IsInBounds(this Vector3 self, Vector3 min, Vector3 max)
        {
            bool isInBounds = true;

            isInBounds &= self.x > min.x && self.x < max.x;
            isInBounds &= self.y > min.y && self.y < max.y;
            isInBounds &= self.z > min.z && self.z < max.z;
            
            return isInBounds;
        }
        
    }
}