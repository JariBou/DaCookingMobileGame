using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GraphicsLabor.Scripts.Core.Utility
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns true if self inherits. Can be used to know if an object can be casted or downCasted to a certain Type
        /// </summary>
        /// <param name="self">The object the method is called on</param>
        /// <param name="parentType">The type to test against</param>
        /// <param name="excludeSelfType">If true, will ignore the object's type when testing</param>
        /// <returns></returns>
        public static bool InheritsFrom(this object self, Type parentType, bool excludeSelfType = false)
        {
            if (self == null) throw new NullReferenceException("Using InheritsFrom on null object");
            return self.GetTypes(excludeSelfType).Contains(parentType);
        }

        ///  <summary>
        /// 		Get type and all base types of target, sorted as following:
        /// 		<para />[self's type, base type, base's base type, ...]
        ///  </summary>
        ///  <param name="self">The object the method is called on</param>
        ///  <param name="excludeSelfType">If set to true will not return this object's type</param>
        ///  <returns></returns>
        public static List<Type> GetTypes(this object self, bool excludeSelfType = false) // Returns object Type along with all parents
        {
            if (self == null) throw new NullReferenceException("Using GetTypes on null object");
            List<Type> types = new List<Type> { excludeSelfType ? self.GetType().BaseType : self.GetType() };

            while (types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            return types;
        }
    }
    
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Returns a Quaternion as a Vector4
        /// </summary>
        /// <param name="quaternion">The quaternion the method is called on</param>
        /// <returns></returns>
        public static Vector4 AsVector4(this Quaternion quaternion)
        {
            return new Vector4(quaternion.x, quaternion.y, quaternion.w, quaternion.z);
        }
    }
}