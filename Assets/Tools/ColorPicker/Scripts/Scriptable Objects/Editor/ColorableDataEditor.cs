using UnityEditor;
using UnityEngine;

namespace Assets.Tools.ColorPicker.Scripts.ScriptableObjects.Editor
{
    [CustomEditor(typeof(ColorableData)), CanEditMultipleObjects]
    public class ColorableDataEditor : UnityEditor.Editor
    {
        private const float SPACING = 15f;

        private SerializedProperty label;
        private SerializedProperty icon;

        private SerializedProperty usesAlternateShaderColor;
        private SerializedProperty shaderMainColorName;

        private SerializedProperty enableExtraProperties;
        private SerializedProperty propertyNames;

        private void OnEnable()
        {
            label = serializedObject.FindProperty("label");
            icon = serializedObject.FindProperty("icon");

            usesAlternateShaderColor = serializedObject.FindProperty("usesAlternateShaderColor");
            shaderMainColorName = serializedObject.FindProperty("shaderMainColorName");

            enableExtraProperties = serializedObject.FindProperty("enableExtraProperties");
            propertyNames = serializedObject.FindProperty("propertyNames");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(label);
            EditorGUILayout.PropertyField(icon);

            GUILayout.Space(SPACING);

            EditorGUILayout.PropertyField(usesAlternateShaderColor);
            GUI.enabled = usesAlternateShaderColor.boolValue;
            EditorGUILayout.PropertyField(shaderMainColorName);
            GUI.enabled = true;

            GUILayout.Space(SPACING);
            
            EditorGUILayout.PropertyField(enableExtraProperties);
            GUI.enabled = enableExtraProperties.boolValue;
            EditorGUILayout.PropertyField(propertyNames);
            GUI.enabled = true;

            serializedObject.ApplyModifiedProperties();
        }
    }
}