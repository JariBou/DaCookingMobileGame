using System;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes;
using GraphicsLabor.Scripts.Editor.Utility.GUI;
using UnityEditor;
using UnityEngine;

namespace GraphicsLabor.Scripts.Editor.Drawers.Decorators
{
    [CustomPropertyDrawer(typeof(ShowMessageAttribute))]
    public class ShowMessageDrawer : DecoratorDrawer {
    
        public override float GetHeight() => GetHelpBoxHeight();
        
        public override void OnGUI(Rect rect)
        {
            if (attribute is not ShowMessageAttribute showMessageAttribute) return;

            float indent = LaborerEditorGUI.GetIndentLength(rect);
            Rect infoBoxRect = new(
                rect.x + indent,
                rect.y,
                rect.width - indent,
                GetHelpBoxHeight());
            
            EditorGUI.HelpBox(infoBoxRect, showMessageAttribute.Message, showMessageAttribute.MessageType switch{
                MessageLevel.Info => MessageType.Info,
                MessageLevel.Warning => MessageType.Warning,
                MessageLevel.Error => MessageType.Error,
                MessageLevel.None => MessageType.None,
                _ => throw new ArgumentOutOfRangeException()
            });
        }
        
        /// <summary>
        /// Returns the height the Help box's text
        /// </summary>
        /// <returns></returns>
        private float GetHelpBoxHeight()
        {
            ShowMessageAttribute showMessageAttributeAttribute = (ShowMessageAttribute)attribute;
            float minHeight = LaborerGUIUtility.MinHelpBoxHeight;
            float desiredHeight = GUI.skin.box.CalcHeight(new GUIContent(showMessageAttributeAttribute.Message), LaborerGUIUtility.CurrentViewWidth);
            float height = Mathf.Max(minHeight, desiredHeight);

            return height;
        }
    }
}
