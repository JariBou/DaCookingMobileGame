using System;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class LabelAttribute: InspectedAttribute
    {
        public readonly string Label;

        /// <summary>
        /// Changes the label of a field or property
        /// </summary>
        /// <param name="label">The new label text</param>
        public LabelAttribute(string label)
        {
            Label = label;
        }
    }
}