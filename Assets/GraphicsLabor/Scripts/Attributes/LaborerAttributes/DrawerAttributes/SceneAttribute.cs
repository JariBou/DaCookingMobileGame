using System;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes
{
    /// <summary>
    /// Used on a string or int field to select a scene name or buildIndex from a dropdown 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SceneAttribute : DrawerAttribute
    {
        // TODO: Animator Param Attribute
    }
}