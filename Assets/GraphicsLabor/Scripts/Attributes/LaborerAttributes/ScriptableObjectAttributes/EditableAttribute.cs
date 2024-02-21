using System;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes
{
    /// <summary>
    /// Makes a ScriptableObject editable in the custom EditorWindow and allows for usage of TabPropertyAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class EditableAttribute : ScriptableObjectAttribute
    {
        
    }
}