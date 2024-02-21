using GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes;
using GraphicsLabor.Scripts.Editor.Utility.GUI;
using GraphicsLabor.Scripts.Editor.Utility.Reflection;
using UnityEditor;
using UnityEngine;

namespace GraphicsLabor.Scripts.Editor.Drawers.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ExpandableAttribute))]
    public class ExpandableDrawer : PropertyDrawerBase
    {
        protected override float GetSelfPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue == null) return GetPropertyHeight(property);

            System.Type propertyType = PropertyUtility.GetPropertyType(property);
            
            if (!typeof(ScriptableObject).IsAssignableFrom(propertyType))
            {
                return GetPropertyHeight(property) + GetHelpBoxHeight();
            }
            
            ScriptableObject scriptableObject = property.objectReferenceValue as ScriptableObject;
            if (!scriptableObject) return GetPropertyHeight(property);

            if (!property.isExpanded) return GetPropertyHeight(property);
            
            using SerializedObject serializedObject = new(scriptableObject);
            float totalHeight = EditorGUIUtility.singleLineHeight;

            using (SerializedProperty iterator = serializedObject.GetIterator())
            {
                if (iterator.NextVisible(true))
                {
                    do
                    {
                        SerializedProperty childProperty = serializedObject.FindProperty(iterator.name);
                                
                        if (childProperty.name.Equals("m_Script", System.StringComparison.Ordinal)) continue;

                        bool visible = PropertyUtility.IsVisible(childProperty);
                        if (!visible) continue;

                        float height = GetPropertyHeight(childProperty);
                        totalHeight += height;
                    } while (iterator.NextVisible(false));
                }
            }

            totalHeight += EditorGUIUtility.standardVerticalSpacing;
            return totalHeight;
        }

        protected override void OnSelfGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);

            if (property.objectReferenceValue == null)
            {
                EditorGUI.PropertyField(rect, property, label, false);
            } else
            {
                System.Type propertyType = PropertyUtility.GetPropertyType(property);
                if (typeof(ScriptableObject).IsAssignableFrom(propertyType))
                {
                    ScriptableObject scriptableObject = property.objectReferenceValue as ScriptableObject;
                    if (!scriptableObject)
                    {
                        EditorGUI.PropertyField(rect, property, label, false);
                    } else
                    {
                        // Draw a foldout
                        Rect foldoutRect = new()
                        {
                            x = rect.x,
                            y = rect.y,
                            width = LaborerGUIUtility.LabelWidth,
                            height = LaborerGUIUtility.SingleLineHeight
                        };
                        
                        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, toggleOnLabelClick: true);


                        // Draw the scriptable object field
                        Rect propertyRect = new()
                        {
                            x = rect.x + LaborerGUIUtility.LabelWidth,
                            y = rect.y,
                            width = rect.width - LaborerGUIUtility.LabelWidth,
                            height = LaborerGUIUtility.SingleLineHeight
                        };

                        property.objectReferenceValue= EditorGUI.ObjectField(propertyRect, "", property.objectReferenceValue, typeof(ScriptableObject), false);
                        //EditorGUI.PropertyField(propertyRect, property, GUIContent.none, false);

                        // Draw the child properties
                        if (property.isExpanded)
                        {
                            DrawChildProperties(rect, property);
                        }
                    }
                }
                else
                {
                    string message = $"{nameof(ExpandableAttribute)} can only be used on scriptable objects";
                    DrawDefaultPropertyAndHelpBox(rect, property, message, MessageType.Warning);
                }
            }

            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Draws a ScriptableObject's child Properties
        /// </summary>
        /// <param name="rect">The Rect where to draw the properties</param>
        /// <param name="property">The SerializedProperty contaiining the ScriptableObject</param>
        private void DrawChildProperties(Rect rect, SerializedProperty property)
        {
            ScriptableObject scriptableObject = property.objectReferenceValue as ScriptableObject;
            if (!scriptableObject) return;

            Rect boxRect = new()
            {
                x = 0.0f,
                y = rect.y + LaborerGUIUtility.SingleLineHeight,
                width = rect.width * 2.0f,
                height = rect.height - LaborerGUIUtility.SingleLineHeight
            };

            GUI.Box(boxRect, GUIContent.none);

            using (new EditorGUI.IndentLevelScope())
            {
                SerializedObject serializedObject = new(scriptableObject);
                serializedObject.Update();

                using (SerializedProperty iterator = serializedObject.GetIterator())
                {
                    float yOffset = LaborerGUIUtility.SingleLineHeight;

                    if (iterator.NextVisible(true))
                    {
                        do
                        {
                            SerializedProperty childProperty = serializedObject.FindProperty(iterator.name);
                            
                            if (childProperty.name.Equals("m_Script", System.StringComparison.Ordinal)) continue;

                            bool visible = PropertyUtility.IsVisible(childProperty);
                            if (!visible) continue;

                            float childHeight = GetPropertyHeight(childProperty);
                            Rect childRect = new()
                            {
                                x = rect.x,
                                y = rect.y + yOffset,
                                width = rect.width,
                                height = childHeight
                            };

                            LaborerEditorGUI.PropertyField(childRect, childProperty, true);

                            yOffset += childHeight;
                        } while (iterator.NextVisible(false));
                    }
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}