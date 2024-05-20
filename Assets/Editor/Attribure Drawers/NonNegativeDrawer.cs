using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(NonNegativeAttribute))]
public class NonNegativePropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create a container to hold the fields
        var container = new VisualElement();

        // Determine the property type and create appropriate field
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            var integerField = new IntegerField(property.displayName)
            {
                value = property.intValue
            };



            // Register callback to enforce non-negative values
            integerField.RegisterValueChangedCallback(evt =>
            {
                if (evt.newValue < 0)
                {
                    integerField.value = 0;
                }
                property.intValue = integerField.value;
                property.serializedObject.ApplyModifiedProperties();
            });
            integerField.AddToClassList("unity-base-field__aligned");
            container.Add(integerField);
        }
        else if (property.propertyType == SerializedPropertyType.Float)
        {
            var floatField = new FloatField(property.displayName)
            {
                value = property.floatValue
            };


            // Register callback to enforce non-negative values
            floatField.RegisterValueChangedCallback(evt =>
            {
                if (evt.newValue < 0f)
                {
                    floatField.value = 0f;
                }
                property.floatValue = floatField.value;
                property.serializedObject.ApplyModifiedProperties();
            });
            floatField.AddToClassList("unity-base-field__aligned");
            container.Add(floatField);
        }
        else
        {
            Debug.LogError("NonNegativeAttribute is only valid on int or float fields.");
        }

        return container;
    }
}
