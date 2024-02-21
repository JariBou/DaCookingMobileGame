using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GraphicsLabor.Scripts.Attributes;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using GraphicsLabor.Scripts.Core.Utility;
using GraphicsLabor.Scripts.Editor.Utility.GUI;
using GraphicsLabor.Scripts.Editor.Utility.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GraphicsLabor.Scripts.Editor.Windows
{
    public class ScriptableObjectEditorWindow : EditorWindowBase
    {
        [SerializeField] private ScriptableObject _selectedScriptableObject;
        [SerializeField] private SerializedObject _serializedObject;
        protected float _totalDrawnHeight = 20f;
        private List<Type> _attributeTypes;

        protected override SerializedObject GetSerializedObject()
        {
            return _serializedObject ??= new SerializedObject(_selectedScriptableObject);
        }
        
        public static void ShowWindow(Object obj)
        {
            if (obj == null || !obj.InheritsFrom(typeof(ScriptableObject)))
            {
                CreateNewEditorWindow<ScriptableObjectEditorWindow>(null, "Scriptable Object Editor");
                GLogger.LogWarning($"Object of type {obj.GetType()} is not assignable to ScriptableObject");
            }
            else
            {
                CreateNewEditorWindow<ScriptableObjectEditorWindow>(obj, "Scriptable Object Editor");
            }
        }

        protected override void OnGUI()
        {
            Rect currentRect = EditorGUILayout.GetControlRect();
            Rect rect2 = currentRect;
            currentRect.width -= LaborerGUIUtility.ScrollBarWidth;
            Rect rect = currentRect;
            rect.height = _totalDrawnHeight;
            rect2.height = position.height;
            
            _scrollPos = GUI.BeginScrollView(rect2, _scrollPos, rect, alwaysShowVertical:true, alwaysShowHorizontal:false);
            
            OnSelfGui(currentRect);
            
            GUI.EndScrollView();
        }

        /// <summary>
        /// Called to draw in the ScrollView
        /// </summary>
        /// <param name="currentRect">The ScrollView Rect</param>
        protected virtual void OnSelfGui(Rect currentRect)
        {
            _totalDrawnHeight = DrawWithRect(currentRect);
            _totalDrawnHeight += LaborerGUIUtility.SingleLineHeight + LaborerGUIUtility.PropertyHeightSpacing;
        }
        
        protected override void PassInspectedObject(Object obj)
        {
            _selectedScriptableObject = (ScriptableObject)obj;
            WindowName = obj != null ? obj.name : "null";
            _serializedObject = new SerializedObject(_selectedScriptableObject);
            GetAttributeTypes();
        }

        /// <summary>
        /// Method called for the button drawn next to the ObjectField containing the selected SO
        /// </summary>
        private void ButtonFunc()
        {
            ScriptableObjectCreatorWindow.ShowWindow(_selectedScriptableObject);
        }

        /// <summary>
        /// Returns all custom ScriptableObjectAttribute of the inspected ScriptableObject. Caches it if possible
        /// </summary>
        /// <returns>A List of all custom ScriptableObjectAttribute</returns>
        private List<Type> GetAttributeTypes()
        {
            if (_attributeTypes == null)
            {
                List<CustomAttributeData> objectCustomAttributes = ReflectionUtility
                    .GetAllAttributesOfObject(_selectedScriptableObject,
                        data => data.AttributeType.IsSubclassOf(typeof(ScriptableObjectAttribute)), true).ToList();
                _attributeTypes = objectCustomAttributes.Select(data => data.AttributeType).ToList();
            }
            return _attributeTypes;
        }

        protected override float DrawWithRect(Rect currentRect)
        {
            GUI.backgroundColor = LaborerGUIUtility.BaseBackgroundColor;
            
            // For now dont allow change of SO if set
            if (GetAttributeTypes().Contains(typeof(ManageableAttribute)))
            {
                LaborerWindowGUI.DrawSoFieldAndButton(currentRect, _selectedScriptableObject, "Open Manager", ButtonFunc);
            }
            else
            {
                using (new EditorGUI.DisabledScope(disabled: true))
                {
                    _selectedScriptableObject = (ScriptableObject)EditorGUI.ObjectField(currentRect, "ScriptableObjectField",
                        _selectedScriptableObject, typeof(ScriptableObject), false);
                }
            }
            
            currentRect.y += LaborerGUIUtility.SingleLineHeight;
            if (_selectedScriptableObject)
            {
                currentRect.y += DrawScriptableObjectWithRect(currentRect, _selectedScriptableObject);
            }
            return currentRect.y;
        }
    }
}