using GraphicsLabor.Scripts.Editor.Utility;
using GraphicsLabor.Scripts.Editor.Utility.GUI;
using UnityEditor;
using UnityEngine;

namespace GraphicsLabor.Scripts.Editor.Windows
{
    public sealed class SettingsEditorWindow : ScriptableObjectEditorWindow
    {
        
        [MenuItem("Window/GraphicLabor/GL Settings")]
        public static void ShowSettings()
        {
            CreateNewEditorWindow<SettingsEditorWindow>(GetSettings(), "GL Settings");
        }

        protected override void OnSelfGui(Rect currentRect)
        {
            _totalDrawnHeight = DrawWithRect(currentRect);
            _totalDrawnHeight += LaborerGUIUtility.PropertyHeightSpacing;

            currentRect.y = _totalDrawnHeight;
            if (GUI.Button(currentRect, "Save Tags"))
            {
                TagGenerator.CreateTagEnumFile();
            }
        }
    }
}