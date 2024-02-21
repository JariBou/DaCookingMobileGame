using System;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes
{
    /// <summary>
    /// Used on ScriptableObjects fields to draw their child properties in a foldout
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ExpandableAttribute : DrawerAttribute
    {
    }
}