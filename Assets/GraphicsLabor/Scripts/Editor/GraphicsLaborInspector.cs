using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GraphicsLabor.Scripts.Attributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using GraphicsLabor.Scripts.Editor.Utility.GUI;
using GraphicsLabor.Scripts.Editor.Utility.Reflection;
using GraphicsLabor.Scripts.Editor.Windows;
using UnityEditor;
using UnityEngine;

namespace GraphicsLabor.Scripts.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public sealed class GraphicsLaborInspector : UnityEditor.Editor
    {
        private List<SerializedProperty> _serializedProperties = new();
        private IEnumerable<PropertyInfo> _properties;
        private IEnumerable<MethodInfo> _methods;
        private bool _isEditableScriptableObject;
        private bool _isManageableScriptableObject;

        private void OnEnable()
        {
            _properties = ReflectionUtility.GetAllProperties(
                target, p => p.GetCustomAttributes(typeof(ShowPropertyAttribute), true).Length > 0);
            
            _methods = ReflectionUtility.GetAllMethods(
                target, m => m.GetCustomAttributes(typeof(ButtonAttribute), true).Length > 0);

            _isEditableScriptableObject = ReflectionUtility
                .GetAllAttributesOfObject(target, c => c.AttributeType == typeof(EditableAttribute), true).Any();
            
            _isManageableScriptableObject = ReflectionUtility
                .GetAllAttributesOfObject(target, c => c.AttributeType == typeof(ManageableAttribute), true).Any();
        }

        public override void OnInspectorGUI()
        {
            GetSerializedProperties(ref _serializedProperties);

            bool anyLaborerAttribute = _serializedProperties.Any(p => PropertyUtility.GetAttribute<ILaborerAttribute>(p) != null);
            if (!anyLaborerAttribute)
            {
                DrawDefaultInspector();
            }
            else
            {
                DrawSerializedProperties();
            }
            DrawProperties();
            DrawButtons();
            
            GUILayout.BeginHorizontal();
            
            if (_isEditableScriptableObject)
            {
                LaborerEditorGUI.CustomPredicateButton("Show Editor", () =>
                {
                    ScriptableObjectEditorWindow.ShowWindow(serializedObject.targetObject);
                });
            }
            if (_isManageableScriptableObject)
            {
                LaborerEditorGUI.CustomPredicateButton("Show Creator", () =>
                {
                    ScriptableObjectCreatorWindow.ShowWindow(serializedObject.targetObject);
                });
            }
            
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Initializes the SerializedPropertiesList
        /// </summary>
        /// <param name="outSerializedProperties">A List of SerializedProperties</param>
        private void GetSerializedProperties(ref List<SerializedProperty> outSerializedProperties)
        {
            outSerializedProperties.Clear();
            using (SerializedProperty iterator = serializedObject.GetIterator())
            {
                if (iterator.NextVisible(true))
                {
                    do
                    {
                        outSerializedProperties.Add(serializedObject.FindProperty(iterator.name));
                    }
                    while (iterator.NextVisible(false));
                }
            }
        }

        /// <summary>
        /// Draws all SerializedProperties
        /// </summary>
        private void DrawSerializedProperties()
        {
            serializedObject.Update();
            
            foreach (SerializedProperty property in _serializedProperties)
            {
                if (property.name.Equals("m_Script", System.StringComparison.Ordinal))
                {
                    using (new EditorGUI.DisabledScope(disabled: true))
                    {
                        EditorGUILayout.PropertyField(property);
                    } continue;
                }
                LaborerEditorGUI.LayoutPropertyField(property);
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draws all properties
        /// </summary>
        private void DrawProperties()
        {
            if (!_properties.Any()) return;
            
            foreach (PropertyInfo property in _properties)
            {
                LaborerEditorGUI.LayoutProperty(serializedObject, property);
            }
        }

        /// <summary>
        /// Draws all buttons
        /// </summary>
        private void DrawButtons()
        {
            if (!_methods.Any()) return;
            
            foreach (MethodInfo method in _methods)
            {
                LaborerEditorGUI.Button(serializedObject.targetObject, method);
            }
        }
    }
}

