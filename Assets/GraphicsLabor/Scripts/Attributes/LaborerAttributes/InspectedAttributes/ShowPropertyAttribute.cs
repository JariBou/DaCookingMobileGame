using System;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ShowPropertyAttribute : InspectedAttribute
    {
        public bool Enabled { get; private set; }
        
        /// <summary>
        /// Shows a property in the Inspector
        /// </summary>
        public ShowPropertyAttribute()
        {
            Enabled = false;
        }

        // <param name="enabled">If true, will allow modification of an Auto-Property</param>
        // public ShowPropertyAttribute(bool enabled = false)
        // {
        //     Enabled = enabled;
        // }
    }
}