using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

namespace GraphicsLabor.Scripts.Editor.Utility
{
    public static class IOHelper
    {
        
        /// <summary>
        /// Creates a folder at parent path
        /// </summary>
        /// <param name="parentFolderPath">Full path of the parent folder</param>
        /// <param name="newFolderName">Name of the folder to be created</param>
        public static void CreateFolder(string parentFolderPath, string newFolderName)
        {
            if (!AssetDatabase.IsValidFolder(Path.Combine(parentFolderPath, newFolderName)))
            {
                AssetDatabase.CreateFolder(parentFolderPath, newFolderName);
                AssetDatabase.Refresh();
            }
        }
        
        /// <summary>
        /// Creates necessary folders to create the given path
        /// Path must start with "Assets/"
        /// </summary>
        /// <param name="fullPath">Full path of folders</param>
        public static void CreateFolder(string fullPath)
        {
            String[] pathParts = fullPath.Split("/");
            String currParentPath = pathParts[0];
            for (int i = 1; i < pathParts.Length; i++)
            {
                CreateFolder(currParentPath, pathParts[i]);
                currParentPath += $"/{pathParts[i]}";
            }
        }
        
        /// <summary>
        /// Creates the asset at path if it cannot load it
        /// </summary>
        /// <param name="obj">Object to create if needed</param>
        /// <param name="path">Path to create the object if needed</param>
        /// <param name="saveAssets">If false, will not call AssetDatabase.SaveAssets()</param>
        public static void CreateAssetIfNeeded(Object obj, string path, bool saveAssets = true)
        {
            CreateAssetIfNeeded<Object>(obj, path, saveAssets);
        }
        /// <summary>
        /// Creates the asset at path if it cannot load it
        /// </summary>
        /// <param name="obj">Object to create if needed</param>
        /// <param name="path">Path to create the object if needed</param>
        /// <param name="saveAssets">If false, will not call AssetDatabase.SaveAssets()</param>
        public static void CreateAssetAndOverride(Object obj, string path, bool saveAssets = true)
        {
            AssetDatabase.CreateAsset(obj, path);

            if (saveAssets)
            {
                AssetDatabase.SaveAssets();
            }
        }
        
        /// <summary>
        /// Creates the asset at path if it cannot load it and returns it cast as T
        /// </summary>
        /// <param name="obj">Object to create if needed</param>
        /// <param name="path">Path to create the object if needed</param>
        /// <param name="saveAssets">If false, will not call AssetDatabase.SaveAssets()</param>
        /// <typeparam name="T">The type to cast returned asset as</typeparam>
        /// <returns></returns>
        public static T CreateAssetIfNeeded<T>(Object obj, string path, bool saveAssets = true) where T : Object
        {
            Object assetAtPath = AssetDatabase.LoadAssetAtPath<Object>(path);

            if (assetAtPath == null)
            {
                AssetDatabase.CreateAsset(obj, path);
                assetAtPath = AssetDatabase.LoadAssetAtPath<Object>(path);
            }

            if (saveAssets)
            {
                AssetDatabase.SaveAssets();
            }

            return (T)assetAtPath;
        }

        /// <summary>
        /// Deletes all non-whitelisted assets from the folders
        /// </summary>
        /// <param name="folders">An Array of strings with the paths to the folders that should be checked</param>
        /// <param name="whiteList">A IEnumerable of strings containing paths to Assets that should not be deleted</param>
        public static void DeleteAssets(string[] folders, IEnumerable<string> whiteList)
        {
            foreach (string asset in AssetDatabase.FindAssets("", folders))
            {
                string path = AssetDatabase.GUIDToAssetPath(asset);
                // ReSharper disable once PossibleMultipleEnumeration
                if (!whiteList.Contains(path))
                {
                    AssetDatabase.DeleteAsset(path);
                }
            }
        }
    }
}
