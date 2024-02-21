using GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes;
using GraphicsLabor.Scripts.Attributes.Utility;
using GraphicsLabor.Scripts.Editor.Utility.GUI;
using UnityEditor;
using UnityEngine;

namespace GraphicsLabor.Scripts.Editor.Drawers.Decorators
{
    [CustomPropertyDrawer(typeof(HorizontalSeparatorAttribute))]
    public class HorizontalSeparatorDrawer : DecoratorDrawer
    {
        public override float GetHeight()
        {
            HorizontalSeparatorAttribute lineAttr = (HorizontalSeparatorAttribute)attribute;
            return LaborerGUIUtility.SingleLineHeight + lineAttr.Height;
        }

        public override void OnGUI(Rect position)
        {
            Rect rect = EditorGUI.IndentedRect(position);
            rect.y += LaborerGUIUtility.LineSeparatorSpacing;
            HorizontalSeparatorAttribute lineAttr = (HorizontalSeparatorAttribute)attribute;
            LaborerEditorGUI.HorizontalLine(rect, lineAttr.Height, lineAttr.Color.GetColor());
        }
    }
}