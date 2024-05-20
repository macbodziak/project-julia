using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(RangeIntAttribute))]
public class RangeIntPropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        RangeIntAttribute range = (RangeIntAttribute)attribute;

        var container = new VisualElement();

        var integerField = new IntegerField(property.displayName);

        integerField.value = property.intValue;

        // Register callback to enforce range limits
        integerField.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue < range.min)
            {
                integerField.value = range.min;
            }
            else if (evt.newValue > range.max)
            {
                integerField.value = range.max;
            }
            else
            {
                property.intValue = evt.newValue;
            }
            property.serializedObject.ApplyModifiedProperties();
        });

        integerField.AddToClassList("unity-base-field__aligned");
        // Add the field to the container
        container.Add(integerField);

        return container;
    }
}