using UnityEditor;
using UnityEngine;

namespace ARP.UWP.Tools.Utility.Editor
{
    [CustomEditor(typeof(ButtonSizer)), CanEditMultipleObjects]
    public class ButtonSizerEditor : UnityEditor.Editor
    {
        SerializedProperty scale;

        private void OnEnable()
        {
            scale = serializedObject.FindProperty("scale");
        }

        public override void OnInspectorGUI()
        {
            ButtonSizer script = (ButtonSizer)target;

            serializedObject.Update();
            EditorGUILayout.PropertyField(scale);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Apply"))
            {
                script.ApplyProperties();
            }
        }
    }
}