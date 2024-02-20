using System;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes
{
    public enum EButtonEnableMode
    {
        Always,
        Editor,
        Playmode
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ButtonAttribute : InspectedAttribute

    {
    public string Text { get; private set; }
    public EButtonEnableMode SelectedEnableMode { get; private set; }

    /// <summary>
    /// Draws a button at the end of the inspector to call the Method
    /// </summary>
    /// <param name="text">The text displayed by the Button. Defaults to the name of the method</param>
    /// <param name="enabledMode">Defines when the button will be drawn (editor, playmode, both)</param>
    public ButtonAttribute(string text = null, EButtonEnableMode enabledMode = EButtonEnableMode.Always)
    {
        Text = text;
        SelectedEnableMode = enabledMode;
    }
    }
}