using UnityEditor;
using UnityEngine;

namespace GraphicsLabor.Scripts.Editor.Utility.GUI
{
    /// <summary>
    /// Helper class holding useful values for GL Custom Editors and Inspector
    /// </summary>
    public static class LaborerGUIUtility
    {
        public static float ScrollBarWidth => 16;
        public static float SingleLineHeight => EditorGUIUtility.singleLineHeight;
        public static float PropertyHeightSpacing => SingleLineHeight * 0.1f;
        public static float LineSeparatorSpacing => SingleLineHeight / 3f;
        public static float LabelWidth => EditorGUIUtility.labelWidth;
        public static float MinHelpBoxHeight => SingleLineHeight;
        public static float CurrentViewWidth => EditorGUIUtility.currentViewWidth;
        public static float SoSelectionButtonHeight => SingleLineHeight + PropertyHeightSpacing*3;

        #region Colors
            public static Color SelectedTabColor => new (0.5f, 0.5f, 0.5f, 1f);
            public static Color SelectedSoTabColor => new (0.7f, 0.7f, 0.7f, 1f);
            public static Color BaseBackgroundColor => new(0.9f, 0.9f, 0.9f);
        #endregion
    }
}