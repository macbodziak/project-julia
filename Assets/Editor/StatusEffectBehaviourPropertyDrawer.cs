using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Rendering;

[CustomPropertyDrawer(typeof(StatusEffectBehaviour))]
public class StatusEffectBehaviourPropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement root = new VisualElement();

        var foldout = new UnityEngine.UIElements.Foldout();
        foldout.Add(new PropertyField(property));

        SerializedObject obj = new SerializedObject(property.objectReferenceValue);
        StatusEffectBehaviour seb = property.objectReferenceValue as StatusEffectBehaviour;
        SerializedProperty remainingDurationProp = obj.FindProperty("remainingDuration");

        var container = new VisualElement();
        Label nameLabel = new Label("<color=#ff5555>" + seb.Name + "</color>   ");
        foldout.text = seb.Name;
        container.Add(nameLabel);

        IntegerField cooldownField = new IntegerField();
        cooldownField.label = "cooldown:";
        cooldownField.SetEnabled(false);
        cooldownField.BindProperty(remainingDurationProp);
        container.Add(cooldownField);

        container.style.flexDirection = FlexDirection.Row;
        container.style.alignItems = Align.Center;

        foldout.Add(container);
        root.Add(foldout);
        return root;
    }
}
