using UnityEditor;
using UnityEngine;

namespace SG.Tools.Utility.Editor
{
    [CustomEditor(typeof(ButtonSizer)), CanEditMultipleObjects]
    public class ButtonSizerEditor : UnityEditor.Editor
    {
        SerializedProperty scale;
        SerializedProperty hasToggle;

        private void OnEnable()
        {
            scale = serializedObject.FindProperty("scale");
            hasToggle = serializedObject.FindProperty("hasToggleState");
        }

        public override void OnInspectorGUI()
        {
            ButtonSizer script = (ButtonSizer)target;

            serializedObject.Update();
            EditorGUILayout.PropertyField(scale);
            EditorGUILayout.PropertyField(hasToggle);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Apply"))
            {
                script.ApplyProperties();
            }
        }
    }
}