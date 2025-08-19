using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Timer))]
public class TimerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        // Draw foldout
        position.height = EditorGUIUtility.singleLineHeight;
        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);

        if (property.isExpanded) {
            EditorGUI.indentLevel++;

            // Get properties
            var maxTimeProp = property.FindPropertyRelative("maxTime");
            var currTimeProp = property.FindPropertyRelative("currTime");

            // Move down
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
        // Always one line if not expanded
        if (!property.isExpanded) {
            return EditorGUIUtility.singleLineHeight;
        }

        // Expanded → foldout + 3 lines (maxTime, currTime, progress bar) with spacing
        int lines = 4; // foldout + 3 fields
        return lines * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
    }
}