using System;
using GraphicsLabor.Scripts.Attributes.Utility;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class HorizontalSeparatorAttribute : DrawerAttribute
    {
        private const float DefaultHeight = 2.0f;
        private const LaborColor DefaultColor = LaborColor.Gray;

        public float Height { get; private set; }
        public LaborColor Color { get; private set; }

        /// <summary>
        /// Draws an HorizontalSeparator 
        /// </summary>
        /// <param name="height">The height of the separator</param>
        /// <param name="color">The color of the separator</param>
        public HorizontalSeparatorAttribute(float height = DefaultHeight, LaborColor color = DefaultColor)
        {
            Height = height;
            Color = color;
        }
    }
}