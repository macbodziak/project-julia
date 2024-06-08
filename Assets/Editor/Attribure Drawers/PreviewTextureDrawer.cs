using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(PreviewTextureAttribute))]
public class PreviewTextureDrawer : PropertyDrawer
{
    SerializedProperty textureProperty;
    VisualElement previewElement;
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        textureProperty = property;
        var container = new VisualElement();

        var propertyField = new PropertyField(property);
        container.Add(propertyField);

        previewElement = new VisualElement();
        previewElement.style.width = 32;
        previewElement.style.height = 32;
        previewElement.style.marginTop = 4;
        previewElement.style.marginBottom = 4;
        previewElement.style.backgroundColor = new StyleColor(Color.grey);

        container.Add(previewElement);

        propertyField.RegisterCallback<ChangeEvent<Object>>(evt => UpdatePreview());

        UpdatePreview();

        return container;
    }

    void UpdatePreview()
    {
        Object obj = textureProperty.objectReferenceValue;

        if (obj is Sprite sprite)
        {
            previewElement.style.backgroundImage = new StyleBackground(sprite.texture);
        }
        else if (obj is Texture2D texture)
        {
            previewElement.style.backgroundImage = new StyleBackground(texture);
        }
        else
        {
            previewElement.style.backgroundImage = null;
        }
    }
}
