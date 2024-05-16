using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ActionDefinition))]
public class ActionDefinitionPropertyDrawer : PropertyDrawer
{
    const int POSITION_OFFSET = 35;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUILayout.PropertyField(property);
        SerializedObject so = null;

        if (property.objectReferenceValue != null)
        {
            so = new SerializedObject(property.objectReferenceValue);
        }
        if (so != null)
        {
            // position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, new GUIContent(so.FindProperty("m_name").stringValue));
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                GUI.enabled = false;

                EditorGUILayout.TextField("Name", so.FindProperty("m_name").stringValue);
                EditorGUILayout.IntField("ACtion Point Cost: ", so.FindProperty("m_actionPointCost").intValue);
                EditorGUILayout.IntField("Power Point Cost: ", so.FindProperty("m_powerPointCost").intValue);
                EditorGUILayout.IntField("Cooldown: ", so.FindProperty("m_cooldown").intValue);
                EditorGUILayout.Space();
                GUI.enabled = true;
                EditorGUI.indentLevel--;
            }
        }
        EditorGUI.EndProperty();
    }
}
