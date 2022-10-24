using UnityEditor;
using UnityEngine;

namespace SG.Tools.Utility.Editor
{
    [CustomEditor(typeof(ScrollableGridObjectCollection))]
    public class ScrollableGridObjectCollectionEditor : UnityEditor.Editor
    {
        private bool foldOut = false;
        private int index = 0;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(25);

            foldOut = EditorGUILayout.Foldout(foldOut, "Debug Settings");
            if (foldOut)
            {
                if (GUILayout.Button("Update Collection"))
                {
                    (target as ScrollableGridObjectCollection).UpdateCollection();
                }

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Index");
                index = EditorGUILayout.IntField(index);
                EditorGUILayout.EndHorizontal();

                if (GUILayout.Button("Update Active Objects"))
                {
                    (target as ScrollableGridObjectCollection).DeactivateAllObjects();
                    (target as ScrollableGridObjectCollection).SetIndex(index);
                }
            }
        }
    }
}