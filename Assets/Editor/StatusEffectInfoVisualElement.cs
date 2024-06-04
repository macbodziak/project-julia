
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class StatusEffectInfoVisualElement : VisualElement
{
    public StatusEffectInfoVisualElement(SerializedObject statusEffectBehaviourObject)
    {
        var root = new VisualElement();

        var durationLabel = new Label();
        durationLabel.name = "durationLabel";
        SerializedProperty durationProperty = statusEffectBehaviourObject.FindProperty("remainingDuration");
        durationLabel.text = "" + durationProperty.intValue;
        durationLabel.BindProperty(durationProperty);
        durationLabel.style.width = 15;

        SerializedObject statusEffectSerializedObject = new SerializedObject(statusEffectBehaviourObject.FindProperty("_statusEffect").objectReferenceValue);
        var nameLabel = new Label("nameLabel");
        nameLabel.name = "nameLabel";
        nameLabel.text = statusEffectSerializedObject.FindProperty("m_name").stringValue;

        var cancelBtn = new Button();
        cancelBtn.name = "cancelBtn";
        cancelBtn.text = "x";

        root.Add(cancelBtn);
        root.Add(durationLabel);
        root.Add(nameLabel);

        root.style.flexDirection = FlexDirection.Row;

        Add(root);

    }
}