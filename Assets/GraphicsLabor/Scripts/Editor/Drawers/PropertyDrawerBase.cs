using GraphicsLabor.Scripts.Editor.Utility.GUI;
using GraphicsLabor.Scripts.Editor.Utility.Reflection;
using UnityEditor;
using UnityEngine;

namespace GraphicsLabor.Scripts.Editor.Drawers
{
    public abstract class PropertyDrawerBase : PropertyDrawer
    {
        // TODO: Resizeable text area
        public sealed override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            bool visible = PropertyUtility.IsVisible(property);
            if (!visible) return;
            
            bool enabled = PropertyUtility.IsEnabled(property);

            using (new EditorGUI.DisabledScope(disabled: !enabled))
            {
                OnSelfGUI(rect, property, PropertyUtility.GetLabel(property));
            }
        }

        /// <summary>
        /// Override this method instead of OnGui to draw the Property
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        protected abstract void OnSelfGUI(Rect rect, SerializedProperty property, GUIContent label);

        /// <summary>
        /// Returns the height of the drawn Property if visible
        /// </summary>
        /// <param name="property">The property</param>
        /// <param name="label">The label of the property</param>
        /// <returns></returns>
        public sealed override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            bool visible = PropertyUtility.IsVisible(property);
            if (!visible) return 0.0f;

            return GetSelfPropertyHeight(property, label);
        }

        /// <summary>
        /// Returns the height of the drawn Property, override this if your property takes more space than usual
        /// </summary>
        /// <param name="property">The property</param>
        /// <param name="label">The label of the property</param>
        /// <returns></returns>
        protected virtual float GetSelfPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, includeChildren: true);
        }
        
        /// <summary>
        /// Returns the height of the drawn Property
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns></returns>
        protected float GetPropertyHeight(SerializedProperty property)
        {
            return EditorGUI.GetPropertyHeight(property, includeChildren: true);
        }

        /// <summary>
        /// Returns the HelpBox Height, override this if your HelpBox is bigger than usual
        /// </summary>
        /// <returns></returns>
        protected virtual float GetHelpBoxHeight()
        {
            return LaborerGUIUtility.SingleLineHeight * 2.0f;
        }

        /// <summary>
        /// Draws the default property with an HelpBox 
        /// </summary>
        /// <param name="rect">The rect for the property</param>
        /// <param name="property">The SerializedProperty</param>
        /// <param name="message">The message displayed in the help box</param>
        /// <param name="messageType">The Severity of the message</param>
        protected void DrawDefaultPropertyAndHelpBox(Rect rect, SerializedProperty property, string message, MessageType messageType)
        {
            float indentLength = LaborerEditorGUI.GetIndentLength(rect);
            Rect helpBoxRect = new(
                rect.x + indentLength,
                rect.y,
                rect.width - indentLength,
                GetHelpBoxHeight());

            LaborerEditorGUI.HelpBox(helpBoxRect, message, messageType, context: property.serializedObject.targetObject);

            Rect propertyRect = new(
                rect.x,
                rect.y + GetHelpBoxHeight(),
                rect.width,
                GetPropertyHeight(property));

            EditorGUI.PropertyField(propertyRect, property, true);
        }
    }
}