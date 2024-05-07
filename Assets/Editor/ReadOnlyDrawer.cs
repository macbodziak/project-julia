using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Disable editing for the property
        GUI.enabled = false;

        // Draw the property field as usual
        EditorGUI.PropertyField(position, property, label, true);

        // Re-enable editing
        GUI.enabled = true;
    }
}
