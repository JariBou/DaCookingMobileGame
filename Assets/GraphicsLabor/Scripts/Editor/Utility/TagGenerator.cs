using System.Collections.Generic;
using System.IO;
using System.Text;
using GraphicsLabor.Scripts.Core.Settings;
using GraphicsLabor.Scripts.Core.Utility;
using UnityEditor;

namespace GraphicsLabor.Scripts.Editor.Utility
{
    public static class TagGenerator
    {
        /// <summary>
        /// Regenerates LaborTag.cs 
        /// </summary>
        public static void CreateTagEnumFile()
        {
            
            GraphicsLaborSettings settings = AssetDatabase.LoadAssetAtPath<GraphicsLaborSettings>("Assets/GraphicsLabor/Scripts/Core/Settings/GraphicsLaborSettings.asset");
            
            StringBuilder content = new();

            // List<string> enumNames = new List<string>
            // {
            //     "First", "Second", "Third"
            // };

            List<string> enumNames = settings._tags;
            content.Append("//\n//\n" + 
                           "// AUTO-GENERATED CODE - DO NOT MODIFY BY HAND!\n" +
                           "//\n" +
                           "// To regenerate this file, look at GraphicLaborer's TagGenerator in Editor\n" +
                           "//\n//\n");
            content.Append("namespace GraphicsLabor.Scripts.Core.Tags\n{\n");
            content.Append("\t[System.Flags] public enum LaborTags\n\t{\n");
            content.Append("\t\tNull = 1 << 0,\n"); // Flags with only 1 value break unity's inspector so we set 1 by default so that
            // When the user adds one it works

            for (int i = 0; i < enumNames.Count; i++)
            {

                content.Append($"\t\t{enumNames[i]} = 1 << {i+1},\n");
            }

            content.Append("\t}\n");
            content.Append("}");
            
            
            IOHelper.CreateFolder(settings._tagsPath); // Just in case
            File.WriteAllText(settings._tagsPath + "/LaborTags.cs", content.ToString());
            
            AssetDatabase.Refresh();
        }
    }
}