using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GraphicsLabor.Scripts.Attributes.LaborerAttributes.ScriptableObjectAttributes;
using GraphicsLabor.Scripts.Core.Utility;
using GraphicsLabor.Scripts.Editor.Utility;
using GraphicsLabor.Scripts.Editor.Utility.GUI;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GraphicsLabor.Scripts.Editor.Windows
{
    public sealed class ScriptableObjectCreatorWindow : EditorWindowBase
    {
        [SerializeField]private ScriptableObject _selectedScriptableObject;
        [SerializeField]private SerializedObject _serializedObject;
        private string _path;
        private string _selectedSoTab = "";
        private float _totalDrawnHeight = 20f;
        private readonly Dictionary<string, ScriptableObject> _soNameAssetDic = new();

        private List<ScriptableObject> _possibleSos = new();

        private Vector2 _cachedTextSize = Vector2.zero;
        
        [MenuItem("Window/GraphicLabor/SO Creator")]
        public static void ShowWindow()
        {
            // _window = GetWindow<ScriptableObjectEditorWindow>();
            // _window.titleContent = new GUIContent("ScriptableObjectEditor");
            // _window._selectedScriptableObject = null;
            // _window.WindowName = "ScriptableObjectEditor";
            CreateNewEditorWindow<ScriptableObjectCreatorWindow>(null, "Scriptable Object Creator").GetPossibleScriptableObjects();
        }
        
        protected override SerializedObject GetSerializedObject()
        {
            return _serializedObject ??= new SerializedObject(_selectedScriptableObject);
        }

        /// <summary>
        /// Returns the _cachedTextSize of the text shown when no SOs with the Manageable attribute exist and calculates it beforehand if necessary 
        /// </summary>
        /// <returns></returns>
        private Vector2 GetCachedTextSize()
        {
            if (_cachedTextSize == Vector2.zero)
            {
                _cachedTextSize = GUI.skin.box.CalcSize(new GUIContent("No Manageable Scriptable Objects"));
            }

            return _cachedTextSize;
        }
        
        /// <summary>
        /// Opens a Object Creator Window
        /// </summary>
        /// <param name="obj">The Object inspected</param>
        public static void ShowWindow(Object obj)
        {
            if (obj == null || !obj.InheritsFrom(typeof(ScriptableObject)))
            {
                CreateNewEditorWindow<ScriptableObjectCreatorWindow>(null, "Scriptable Object Creator").GetPossibleScriptableObjects();
                GLogger.LogWarning($"Object of type {obj.GetType()} is not assignable to ScriptableObject");
            }
            else
            {
                CreateNewEditorWindow<ScriptableObjectCreatorWindow>(obj, "Scriptable Object Creator").GetPossibleScriptableObjects();
            }
        }

        protected override void OnGUI()
        {
            Rect currentRect = EditorGUILayout.GetControlRect();

            if (GetPossibleScriptableObjects().Count == 0)
            {
                Rect middleText = currentRect;
                Vector2 textSize = GetCachedTextSize();
                middleText.x = middleText.width / 2 - textSize.x/2;
                middleText.y = position.height / 2 - textSize.y / 2;
                
                EditorGUI.LabelField(middleText, "No Manageable Scriptable Objects");
            }
            else
            {
                Rect selectionTabRect = currentRect;

                int tabSelectionWidth = 150;
                selectionTabRect.width = tabSelectionWidth - 10;
            
                currentRect.x = tabSelectionWidth;
                currentRect.width -= tabSelectionWidth;
            
                Rect scrollViewRect = currentRect;
                currentRect.width -= LaborerGUIUtility.ScrollBarWidth;
                Rect viewRect = currentRect;
                viewRect.height = _totalDrawnHeight;
                scrollViewRect.height = position.height;
                
                selectionTabRect.y += CreateSoSelectionButtons(selectionTabRect);
            
                // SO display
            
                _scrollPos = GUI.BeginScrollView(scrollViewRect, _scrollPos, viewRect, alwaysShowVertical:true, alwaysShowHorizontal:false);
            
                _totalDrawnHeight = DrawWithRect(currentRect);
                _totalDrawnHeight += LaborerGUIUtility.SingleLineHeight + LaborerGUIUtility.PropertyHeightSpacing;
            
                GUI.EndScrollView();
            }
            
        }

        /// <summary>
        /// Creates the Buttons used to select which SO to create
        /// </summary>
        /// <param name="selectionTabRect">The Rect for the buttons</param>
        /// <returns></returns>
        private float CreateSoSelectionButtons(Rect selectionTabRect)
        {
            float yOffset = 0;

            for (int i = 0; i < _soNameAssetDic.Keys.Count; i++)
            {
                Rect buttonRect = new()
                {
                    x =selectionTabRect.x,
                    y = selectionTabRect.y + yOffset,
                    width = selectionTabRect.width,
                    height = LaborerGUIUtility.SingleLineHeight
                };
                
                string soName = _soNameAssetDic.Keys.ToArray()[i];
                
                GUI.SetNextControlName(soName);
                if (_selectedSoTab == soName)
                {
                    GUI.backgroundColor = LaborerGUIUtility.SelectedSoTabColor;
                }
                if (GUI.Button(buttonRect, new GUIContent(GetTruncatedTempSoName(soName), GetTempSoName(soName)) , EditorStyles.toolbarButton))
                {
                    SelectSo(soName);
                    GUI.FocusControl(soName);
                }

                GUI.backgroundColor = LaborerGUIUtility.BaseBackgroundColor;
                
                yOffset += LaborerGUIUtility.SoSelectionButtonHeight;
            }
            
            return yOffset;
        }

        /// <summary>
        /// Changes the inspected SO
        /// </summary>
        /// <param name="soName">The name of the SO to select</param>
        private void SelectSo(string soName)
        {
            _selectedSoTab = soName;
            _selectedScriptableObject = _soNameAssetDic[soName];
            _serializedObject = new SerializedObject(_soNameAssetDic[soName]);
        }
        
        /// <summary>
        /// Returns a Truncated SO Name to 20chars+"..." if longer than 22 chars
        /// </summary>
        /// <param name="soName">The name to be truncated</param>
        /// <returns></returns>
        private static string GetTruncatedTempSoName(string soName)
        {
            string newSoName = GetTempSoName(soName);
            if (newSoName.Length > 22)
            {
                return $"{newSoName[..20].Trim()}...";
            }

            return newSoName;
        }

        /// <summary>
        /// Returns the Nicifyed name of a temp SO without the "_Temp" at the end if needed
        /// </summary>
        /// <param name="soName">The SO name to change</param>
        /// <returns></returns>
        private static string GetTempSoName(string soName)
        {
            string newSoName = ObjectNames.NicifyVariableName(soName);
            if (newSoName.EndsWith("Temp"))
            {
                newSoName = newSoName.Remove(newSoName.Length - "_Temp".Length);
            }
            return newSoName;
        }

        /// <summary>
        /// Selects the SO by passing the object to inspect, if doesn't exist will do nothing
        /// </summary>
        /// <param name="obj">The SO to inspect</param>
        protected override void PassInspectedObject(Object obj)
        {
            if (obj == null) return;

            string soTypeName = obj.GetType().Name;

            if (GetPossibleScriptableObjects().Exists(so => GetSoClassName(so) == soTypeName))
            {
                SelectSo(soTypeName);
            }
        }

        /// <summary>
        /// Method called for the button drawn next to the ObjectField containing the selected SO
        /// </summary>
        private void ButtonFunc()
        {
            ScriptableObject obj = _soNameAssetDic[_selectedSoTab];
            ScriptableObject so = CreateInstance(obj.GetType());
            so.name = GetSoClassName(so);
            IOHelper.CreateFolder(GetSettings()._tempScriptableObjectsPath);
            IOHelper.CreateAssetAndOverride(so, $"{GetSettings()._tempScriptableObjectsPath}/{so.name}_temp.asset");
            _soNameAssetDic[GetSoClassName(so)] = so;
            SelectSo(GetSoClassName(so));
            GUI.FocusControl(null);
            foreach (string key in _soNameAssetDic.Keys)
            {
                GLogger.Log($"Key: {key}");
            }
        }

        /// <summary>
        /// Returns the class name of the ScriptableObject
        /// </summary>
        /// <param name="scriptableObject"></param>
        /// <returns></returns>
        private string GetSoClassName(ScriptableObject scriptableObject)
        {
            return scriptableObject.GetType().Name;
        }
        
        protected override float DrawWithRect(Rect currentRect)
        {
            GUI.backgroundColor = LaborerGUIUtility.BaseBackgroundColor;
            
            LaborerWindowGUI.DrawSoFieldAndButton(currentRect, _selectedScriptableObject, "Reset Values", ButtonFunc);
            currentRect.y += LaborerGUIUtility.SingleLineHeight;
            
            if (_selectedScriptableObject)
            {
                currentRect.y += DrawScriptableObjectWithRect(currentRect, _selectedScriptableObject);
            }
            
            if (GUI.Button(currentRect, "Save As"))
            {
                //String tempPath = EditorUtility.OpenFolderPanel("Save ScriptableObject at:", "", "");
                string tempPath =
                    EditorUtility.SaveFilePanelInProject("Save Asset", GetTempSoName(_selectedSoTab), "asset", "Enter the new SO Name");
                if (tempPath.StartsWith("Assets")) {
                    //GetSettings()._tempScriptableObjectsPath = tempPath;
                    ScriptableObject obj = Instantiate(_soNameAssetDic[_selectedSoTab]);
                    string objName = tempPath.Split('/')[^1].Replace(".asset", "");
                    obj.name = objName;
                    IOHelper.CreateAssetAndOverride(obj, tempPath);
                }
            }
            currentRect.y += LaborerGUIUtility.SingleLineHeight;
            
            return currentRect.y;
        }
        
        /// <summary>
        /// Looks through assemblies to find ScriptableObjects with the ManageableAttribute and returns that list.
        /// Also Initializes the Dictionary containing the SONames and the SOs Caches it when possible. 
        /// </summary>
        /// <returns>A List of Scriptable Objects with the Manageable Attribute</returns>
        private List<ScriptableObject> GetPossibleScriptableObjects()
        {
            if (_possibleSos != null && _possibleSos.Count != 0)
            {
                foreach (ScriptableObject so in _possibleSos.Where(so => !_soNameAssetDic.ContainsKey(GetSoClassName(so))))
                {
                    _soNameAssetDic.Add(GetSoClassName(so), so);
                }
                return _possibleSos;
            }
            
            IEnumerable<Type> assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.GetName().ToString().StartsWith("Unity"))
                .SelectMany(a => a.GetTypes().Where(t => t.IsDefined(typeof(ManageableAttribute)) && !t.IsAbstract));

            IEnumerable<Type> types = assemblies as Type[] ?? assemblies.ToArray();
            
            _possibleSos = new List<ScriptableObject>(types.Count());
            List<Type> typesList = new List<Type>();
            List<string> soPaths = new List<string>();
            foreach (Type type in types)
            {
                if (!type.IsSubclassOf(typeof(ScriptableObject)) || typesList.Contains(type)) continue;
                
                ScriptableObject so = CreateInstance(type);
                so.name = GetSoClassName(so);
                    
                string soPath = $"{GetSettings()._tempScriptableObjectsPath}/{so.name}_temp.asset";
                IOHelper.CreateFolder(GetSettings()._tempScriptableObjectsPath);
                so = IOHelper.CreateAssetIfNeeded<ScriptableObject>(so, soPath, false);
                
                soPaths.Add(soPath);
                _possibleSos.Add(so);
                typesList.Add(type);
                _soNameAssetDic.Add(GetSoClassName(so), so);
            }
            AssetDatabase.SaveAssets();
            
            // Remove unused SOs
            string[] folders = { GetSettings()._tempScriptableObjectsPath };
            IOHelper.DeleteAssets(folders, soPaths);

            if (_soNameAssetDic.Keys.Count == 0) return new List<ScriptableObject>();
            
            // Default select the first SO, should go somewhere else
            SelectSo(_soNameAssetDic.Keys.ToArray()[0]);

            return _possibleSos;
        }
    }
}