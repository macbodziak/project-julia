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
        foldout.text = "status effect";

        foldout.Add(new PropertyField(property));
        SerializedObject obj = new SerializedObject(property.objectReferenceValue);
        StatusEffectBehaviour seb = property.objectReferenceValue as StatusEffectBehaviour;
        SerializedProperty StatusEffectProp = obj.FindProperty("_statusEffect");
        SerializedProperty remainingDurationProp = obj.FindProperty("remainingDuration");
        var container = new VisualElement();
        Label nameLabel = new Label("<color=#ff5555>" + seb.Name + "</color>   ");
        container.Add(nameLabel);
        IntegerField cooldownField = new IntegerField();
        cooldownField.label = "cooldown:";
        cooldownField.style.color = new StyleColor(new UnityEngine.Color(1f, 022f, 0.2f));
        cooldownField.SetEnabled(false);
        cooldownField.BindProperty(remainingDurationProp);
        container.Add(cooldownField);
        container.style.flexDirection = FlexDirection.Row;
        container.style.alignItems = Align.Center;
        foldout.Add(container);
        // foldout.Add(cooldownField);
        root.Add(foldout);
        return root;
    }
}
