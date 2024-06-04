using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(RequiredFieldAttribute))]
public class RequiredFieldDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create a container for the property and the warning message
        var container = new VisualElement();

        // Create a property field and add it to the container
        var propertyField = new PropertyField(property);
        propertyField.AddToClassList("unity-base-field__aligned");
        container.Add(propertyField);

        // Create a horizontal container for the icon and the warning message
        var warningContainer = new VisualElement();
        warningContainer.style.flexDirection = FlexDirection.Row;
        warningContainer.style.alignContent = Align.FlexStart;

        // Create an image element for the warning icon
        var warningIcon = new Image();
        warningIcon.image = EditorGUIUtility.IconContent("console.warnicon").image;
        warningIcon.style.width = 16;
        warningIcon.style.height = 16;
        warningIcon.style.marginRight = 4;
        warningContainer.Add(warningIcon);

        // Create a label for the warning message
        var warningLabel = new Label();
        warningLabel.style.color = new Color(1f, 0.33f, 0.33f);
        // warningLabel.style.display = DisplayStyle.None; // Initially hidden
        warningLabel.text = property.displayName + " field not set!";
        warningLabel.AddToClassList("unity-base-field__aligned");
        warningContainer.Add(warningLabel);


        container.Add(warningContainer);
        // Register a callback to update the warning message visibility
        propertyField.RegisterValueChangeCallback(evt =>
        {
            UpdateWarningMessageVisibility(property, warningContainer);
        });

        // Initial update for the warning message visibility
        UpdateWarningMessageVisibility(property, warningContainer);

        return container;
    }

    private void UpdateWarningMessageVisibility(SerializedProperty property, VisualElement warningContianer)
    {
        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
        {
            warningContianer.style.display = DisplayStyle.Flex;
        }
        else if (property.propertyType == SerializedPropertyType.String && property.stringValue == "")
        {
            warningContianer.style.display = DisplayStyle.Flex;
        }
        else
        {
            warningContianer.style.display = DisplayStyle.None;
        }
    }
}
