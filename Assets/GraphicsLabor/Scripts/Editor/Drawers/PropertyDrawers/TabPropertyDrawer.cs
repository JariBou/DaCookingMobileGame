using System.Linq;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using GraphicsLabor.Scripts.Core.Utility;
using GraphicsLabor.Scripts.Editor.Utility.GUI;
using GraphicsLabor.Scripts.Editor.Utility.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GraphicsLabor.Scripts.Editor.Drawers.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(TabPropertyAttribute))]
    public class TabPropertyDrawer : PropertyDrawerBase
    {
        protected override void OnSelfGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);
            
            Object obj = property.serializedObject.targetObject;
            
            if (!obj.GetTypes().Contains(typeof(ScriptableObject)) || !ReflectionUtility.GetAllAttributesOfObject(obj, data => data.AttributeType == typeof(EditableAttribute), true).Any())
            {
                DrawDefaultPropertyAndHelpBox(rect, property, "TabProperty Attribute only works on ScriptableObjects with the Editable Attribute", MessageType.Warning);
                //Debug.LogWarning("TabProperty Attribute only works on ScriptableObjects with the Attribute Editable");
            }
            else
            {
                LaborerEditorGUI.PropertyField(rect, property, true);
            }

            EditorGUI.EndProperty();
        }

        protected override float GetSelfPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Object obj = property.serializedObject.targetObject;
            
            if (!obj.GetTypes().Contains(typeof(ScriptableObject)) || !ReflectionUtility.GetAllAttributesOfObject(obj, data => data.AttributeType == typeof(EditableAttribute), true).Any())
            {
                return GetPropertyHeight(property) + GetHelpBoxHeight();
            }
            
            return GetPropertyHeight(property);
        }
    }
}