using UnityEngine;

namespace GraphicsLabor.Scripts.Attributes.Utility
{
    /// <summary>
    /// UnityEngine.Color counterparts for usage with CustomAttributes
    /// </summary>
    public enum LaborColor
    {
        Clear,
        White,
        Black,
        Gray,
        Red,
        Pink,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }

    public static class LaborColorExtension
    {
        /// <summary>
        /// Returns a UnityEngine.Color given its LaborColor counterpart 
        /// </summary>
        /// <param name="color">The color expected</param>
        /// <returns></returns>
        public static Color GetColor(this LaborColor color)
        {
            // Unfortunately Color class field colors are not known at this compile time
            return color switch
            {
                LaborColor.Clear => new Color32(0, 0, 0, 0),
                LaborColor.White => new Color32(255, 255, 255, 255),
                LaborColor.Black => new Color32(0, 0, 0, 255),
                LaborColor.Gray => new Color32(128, 128, 128, 255),
                LaborColor.Red => new Color32(255, 0, 63, 255),
                LaborColor.Pink => new Color32(255, 152, 203, 255),
                LaborColor.Orange => new Color32(255, 128, 0, 255),
                LaborColor.Yellow => new Color32(255, 211, 0, 255),
                LaborColor.Green => new Color32(98, 200, 79, 255),
                LaborColor.Blue => new Color32(0, 135, 189, 255),
                LaborColor.Indigo => new Color32(75, 0, 130, 255),
                LaborColor.Violet => new Color32(128, 0, 255, 255),
                _ => new Color32(0, 0, 0, 255)
            };
        }
    }
    
}