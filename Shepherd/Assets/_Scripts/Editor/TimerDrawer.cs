using UnityEditor;
using UnityEngine;

namespace Utilities
{
    [CustomPropertyDrawer(typeof(Timer))]
    public class TimerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);

            if (property.isExpanded) {
                EditorGUI.indentLevel++;

                var maxTimeProp = property.FindPropertyRelative("maxTime");
                var currTimeProp = property.FindPropertyRelative("currTime");

                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(position, maxTimeProp);

                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(position, currTimeProp);

                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.ProgressBar(position, currTimeProp.floatValue / Mathf.Max(0.01f, maxTimeProp.floatValue), "Progress");

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (!property.isExpanded) {
                return EditorGUIUtility.singleLineHeight;
            }
            return 4 * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
        }
    }
}