using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GraphicsLabor.Scripts.Core.Utility;
using GraphicsLabor.Scripts.Editor.Settings;
using GraphicsLabor.Scripts.Editor.Utility.GUI;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Object = UnityEngine.Object;


namespace GraphicsLabor.Scripts.Editor.Windows
{
    public abstract class EditorWindowBase : WindowBase
    {
        [SerializeField] protected string _selectedPropTab = "";
        [SerializeField] protected Vector2 _scrollPos;

        /// <summary>
        /// Called to pass the Inspected Object
        /// </summary>
        /// <param name="obj">Inspected Object</param>
        protected abstract void PassInspectedObject(Object obj);

        /// <summary>
        /// Returns the SerializedObject or creates a new one based on the selected ScriptableObjected
        /// </summary>
        /// <returns></returns>
        protected abstract SerializedObject GetSerializedObject();

        /// <summary>
        /// Calls Methods to draw Parts of the editor
        /// </summary>
        /// <param name="currentRect">The current Rect of the window</param>
        /// <returns>The sum height of all draws</returns>
        protected abstract float DrawWithRect(Rect currentRect);
        
        /// <summary>
        /// Creates and returns a new EditorWindow 
        /// </summary>
        /// <param name="obj">The Object the editor will be inspecting</param>
        /// <param name="displayName">The display name of the window</param>
        /// <typeparam name="T">The type of the editor window</typeparam>
        /// <returns></returns>
        protected static T CreateNewEditorWindow<T>(Object obj, string displayName = "EditorWindowBase") where T : EditorWindowBase
        {
            WindowBase window = null;
            WindowSettings settings = GetWindowSettings();

            if (settings.OpenedCustomWindows.Count != 0)
            {
                window = settings.FindWindowWhere(customEditor =>
                    (customEditor.WindowName == (obj != null ? obj.name : "null") || customEditor.WindowName == displayName) && customEditor.SelfType == typeof(T));
            }
            
            if (window == null)
            {
                window = CreateAndInitWindow<T>(obj, displayName, typeof(T));
            }
            else
            {
                (window as T)?.PassInspectedObject(obj);
            }

            window.Focus();
            return window as T;
        }

        /// <summary>
        /// Creates and inits a new EditorWindow
        /// </summary>
        /// <param name="obj">The inspected object</param>
        /// <param name="displayName">The window's display name</param>
        /// <param name="desiredDockNextTo">The type of window to dock next to</param>
        /// <typeparam name="T">The type of the editor window</typeparam>
        /// <returns></returns>
        private static T CreateAndInitWindow<T>(Object obj, string displayName, params Type[] desiredDockNextTo) where T : EditorWindowBase
        {
            T window = WindowBase.CreateAndInitWindow<T>(displayName, desiredDockNextTo);
             window.PassInspectedObject(obj);
            return window;
        }

        /// <summary>
        /// Called on recompilation in the editor to reset windowBase.SelfType lost during recompilation
        /// </summary>
        [DidReloadScripts]
        private static void OnScriptReloadSelf()
        {
            WindowSettings settings = GetWindowSettings();
            if (settings.OpenedCustomWindows.Count == 0)
            {
                settings.OpenedCustomWindows.AddRange(Resources.FindObjectsOfTypeAll<WindowBase>());

                foreach (WindowBase windowBase in settings.OpenedCustomWindows)
                {
                    windowBase.SetType(windowBase.GetType());
                }
            }
        }
        
        /// <summary>
        /// Draws a ScriptableObject with given startRect for position
        /// </summary>
        /// <param name="startRect">The Rect representing the starting position</param>
        /// <param name="scriptableObject">The Scriptable Object to draw</param>
        /// <returns>The sum height of all draws</returns>
        protected float DrawScriptableObjectWithRect(Rect startRect, ScriptableObject scriptableObject)
        {
            if (!scriptableObject) return 0f;
            Dictionary<string, List<SerializedProperty>> tabbedSerializedProperties = new Dictionary<string, List<SerializedProperty>>();
            Dictionary<string, List<PropertyInfo>> tabbedProperties = new Dictionary<string, List<PropertyInfo>>();

            SerializedObject serializedObject = GetSerializedObject();
            serializedObject.Update();
            float yOffset = LaborerGUIUtility.PropertyHeightSpacing;

            yOffset += LaborerWindowGUI.DrawScriptableObjectNormalSerializedFields(startRect, yOffset, serializedObject, ref tabbedSerializedProperties);
            yOffset += LaborerWindowGUI.DrawScriptableObjectNormalProperties(startRect, yOffset, serializedObject, ref tabbedProperties);

            IEnumerable<string> tabs = GHelpers.ConcatenateLists(tabbedSerializedProperties.Keys, tabbedProperties.Keys).ToArray();
            float buttonWidth = startRect.width / tabs.Count();
            int i = 0;
            foreach (string key in tabs)
            {
                Rect buttonRect = new()
                {
                    x =startRect.x + buttonWidth * i,
                    y = startRect.y + yOffset,
                    width = buttonWidth,
                    height = LaborerGUIUtility.SingleLineHeight
                };
                if (key == _selectedPropTab)
                {
                    GUI.backgroundColor = LaborerGUIUtility.SelectedTabColor;
                } 
                if (GUI.Button(buttonRect, key, EditorStyles.toolbarButton))
                {
                    _selectedPropTab = key == _selectedPropTab ? "" : key;
                }

                GUI.backgroundColor = LaborerGUIUtility.BaseBackgroundColor;
                
                i++;
            }
            yOffset += LaborerGUIUtility.SingleLineHeight + LaborerGUIUtility.PropertyHeightSpacing*2;

            if (tabbedSerializedProperties.TryGetValue(_selectedPropTab, out var serializedProperties))
            {
                yOffset += LaborerWindowGUI.DrawScriptableObjectTabbedSerializedFields(startRect, yOffset, serializedProperties);
            }
            if (tabbedProperties.TryGetValue(_selectedPropTab, out var normalProperties))
            {
                yOffset += LaborerWindowGUI.DrawScriptableObjectTabbedProperties(startRect, yOffset, serializedObject, normalProperties);
            }
            yOffset += LaborerGUIUtility.PropertyHeightSpacing;
            
            serializedObject.ApplyModifiedProperties();
            return yOffset;
        }
    }
}