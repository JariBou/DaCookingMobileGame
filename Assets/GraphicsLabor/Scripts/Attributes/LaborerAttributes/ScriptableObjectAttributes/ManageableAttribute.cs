using System;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes
{
    /// <summary>
    /// Makes this ScriptableObject visible to the ScriptableObjectCreatorWindow
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ManageableAttribute : ScriptableObjectAttribute
    {
        
    }
}