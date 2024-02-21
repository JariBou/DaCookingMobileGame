using GraphicsLabor.Scripts.Core.Tags.Components;
using UnityEditor;
using UnityEngine;

namespace GraphicsLabor.Scripts.Editor.Tags
{
    [CustomEditor(typeof(LinkedLaborTagComponent)), CanEditMultipleObjects]
    public class LinkedLaborTagInspector : UnityEditor.Editor
    {
        private SerializedProperty _tags;
        private SerializedProperty _linkedObject;
        private SerializedProperty _linkedScript;
        private SerializedProperty _linkObject;
        private SerializedProperty _linkScript;
        void OnEnable()
        {
            // Setup the SerializedProperties.
            _tags = serializedObject.FindProperty("_tags");
            _linkedObject = serializedObject.FindProperty("_linkedGameObject");
            _linkedScript = serializedObject.FindProperty("_linkedMonoBehaviour");
            _linkObject = serializedObject.FindProperty("_linkGameObject");
            _linkScript = serializedObject.FindProperty("_linkMonoBehaviour");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // _tags.enumValueFlag = (int)(LaborTags)EditorGUILayout.EnumFlagsField(_tags.displayName, (LaborTags)_tags.enumValueFlag, GUILayout.Width(width/3), GUILayout.ExpandWidth(false)); // This works smh
            EditorGUILayout.PropertyField(_tags, new GUIContent("Tags"));
            
            EditorGUILayout.BeginHorizontal();
            float width = Screen.width;
            _linkObject.boolValue = EditorGUILayout.ToggleLeft("Link Object", _linkObject.boolValue, GUILayout.Width(width/2), GUILayout.ExpandWidth(false));
            _linkScript.boolValue = EditorGUILayout.ToggleLeft("Link Script", _linkScript.boolValue, GUILayout.Width(width/2), GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();
            
            if (_linkObject.boolValue) EditorGUILayout.PropertyField(_linkedObject);
            if (_linkScript.boolValue) EditorGUILayout.PropertyField(_linkedScript);

            serializedObject.ApplyModifiedProperties();
        }

    }
}
