using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GraphicsLabor.Scripts.Core.Utility;
using UnityEngine;

namespace GraphicsLabor.Scripts.Editor.Utility.Reflection
{
    /// <summary>
    /// Helper class used for reflection on objects
    /// </summary>
    public static class ReflectionUtility
    {
        /// <summary>
        /// Returns all Fields of a given object filtering with a predicate
        /// </summary>
        /// <param name="target">The object to scan</param>
        /// <param name="predicate">A function acting as a filter</param>
        /// <returns></returns>
        public static IEnumerable<FieldInfo> GetAllFields(object target, Func<FieldInfo, bool> predicate)
        {
            if (target == null)
            {
                Debug.LogError("The target object is null. Check for missing scripts.");
                yield break;
            }
            
            List<Type> types = target.GetTypes();

            for (int i = types.Count - 1; i >= 0; i--)
            {
                IEnumerable<FieldInfo> fieldInfos = types[i]
                    .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(predicate);

                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    yield return fieldInfo;
                }
            }
        }

        /// <summary>
        /// Returns all Properties of a given object filtering with a predicate
        /// </summary>
        /// <param name="target">The object to scan</param>
        /// <param name="predicate">A function acting as a filter</param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetAllProperties(object target, Func<PropertyInfo, bool> predicate)
        {
            if (target == null)
            {
                Debug.LogError("The target object is null. Check for missing scripts.");
                yield break;
            }

            List<Type> types = target.GetTypes();

            for (int i = types.Count - 1; i >= 0; i--)
            {
                IEnumerable<PropertyInfo> propertyInfos = types[i]
                    .GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(predicate);

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    yield return propertyInfo;
                }
            }
        }

        /// <summary>
        /// Returns all Methods of a given object filtering with a predicate
        /// </summary>
        /// <param name="target">The object to scan</param>
        /// <param name="predicate">A function acting as a filter</param>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> GetAllMethods(object target, Func<MethodInfo, bool> predicate)
        {
            if (target == null)
            {
                Debug.LogError("The target object is null. Check for missing scripts.");
                yield break;
            }

            List<Type> types = target.GetTypes();

            for (int i = types.Count - 1; i >= 0; i--)
            {
                IEnumerable<MethodInfo> methodInfos = types[i]
                    .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(predicate);

                foreach (MethodInfo methodInfo in methodInfos)
                {
                    yield return methodInfo;
                }
            }
        }
        
        /// <summary>
        /// Returns all Attributes of a given object, filtering with a predicate
        /// </summary>
        /// <param name="target">The object to scan</param>
        /// <param name="predicate">A function acting as a filter</param>
        /// <param name="inherit">If true, will also return inherited Attributes</param>
        /// <returns></returns>
        public static IEnumerable<CustomAttributeData> GetAllAttributesOfObject(object target, Func<CustomAttributeData, bool> predicate, bool inherit = false)
        {
            if (target == null)
            {
                Debug.LogError("The target object is null. Check for missing scripts.");
                yield break;
            }
            
            List<Type> types = target.GetTypes();

            for (int i = inherit ? types.Count - 1 : 0; i >= 0; i--)
            {
                IEnumerable<CustomAttributeData> fieldInfos = types[i].CustomAttributes;

                foreach (CustomAttributeData customAttributeData in fieldInfos)
                {
                    //Debug.Log($"Invoking with type: {customAttributeData.AttributeType}");
                    if (predicate.Invoke(customAttributeData))
                    {
                        //Debug.Log($"Yielded attributeDataType: {customAttributeData.AttributeType}");
                        yield return customAttributeData;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the Field with given name of target
        /// </summary>
        /// <param name="target">The object to scan</param>
        /// <param name="fieldName">The Field Name</param>
        /// <returns></returns>
        public static FieldInfo GetField(object target, string fieldName)
        {
            return GetAllFields(target, f => f.Name.Equals(fieldName, StringComparison.Ordinal)).FirstOrDefault();
        }

        /// <summary>
        /// Returns the Property with given name of target
        /// </summary>
        /// <param name="target">The object to scan</param>
        /// <param name="propertyName">The Property Name</param>
        /// <returns></returns>
        public static PropertyInfo GetProperty(object target, string propertyName)
        {
            return GetAllProperties(target, p => p.Name.Equals(propertyName, StringComparison.Ordinal)).FirstOrDefault();
        }

        /// <summary>
        /// Returns the Method with given name of target
        /// </summary>
        /// <param name="target">The object to scan</param>
        /// <param name="methodName">The Method Name</param>
        /// <returns></returns>
        public static MethodInfo GetMethod(object target, string methodName)
        {
            return GetAllMethods(target, m => m.Name.Equals(methodName, StringComparison.Ordinal)).FirstOrDefault();
        }
    }
}
