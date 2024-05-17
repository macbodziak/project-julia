using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(ActionDefinition))]
public class ActionDefinitionPropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement propertyRoot = new VisualElement();

        if (property.objectReferenceValue == null)
        {
            propertyRoot.Add(new Label("ScriptableObject reference is null."));
            return propertyRoot;
        }

        propertyRoot.Add(new PropertyField(property));

        // Get the serialized object from the property
        SerializedObject serializedObject = new SerializedObject(property.objectReferenceValue);

        Foldout foldout = new Foldout();
        foldout.text = "Action Definition details:";

        // Iterate through the properties of the serialized object
        SerializedProperty iterator = serializedObject.GetIterator();
        iterator.NextVisible(true); // Skip generic field

        while (iterator.NextVisible(false))
        {
            // Create a property field for each property and add it to the root
            var field = new PropertyField(iterator);
            field.Bind(serializedObject);
            foldout.Add(field);
        }

        propertyRoot.Add(foldout);
        return propertyRoot;
    }
}
